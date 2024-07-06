using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Exceptions;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Domain.Settings;
using WorkOrder.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WorkOrder.Domain.Services;

public class UserService(IUserRepository _repository, AppSetting _appSettings, IHttpContextAccessor _httpContextAccessor) : BaseService<UserModel>(_repository, _httpContextAccessor), IUserService
{
    public async Task AddUserAsync(UserModel user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
        await AddAsync(user);
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
        await UpdateAsync(user);
    }

    public async Task UpdatePhoneNumberAsync(int id, string phoneNumber)
    {
        var model = await GetByIdAsync(id);
        model.PhoneNumber = phoneNumber;
        model.DataAlteracao = DateTime.Now;
        model.UpdatedBy = UserId;
        await _repository.EditAsync(model);
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        if (UserPerfil == ConstantUtil.ClientProfileName)
            return await GetAllAsync(x => x.Active && x.Id == UserId);
        else
            return await GetAllAsync();
    }

    public async Task<UserModel> GetByIdUserAsync(int id)
    {
        if (UserPerfil == ConstantUtil.ClientProfileName)
            return await GetAsync(x => x.Id == id && x.Active && x.Id == UserId);
        else
            return await GetByIdAsync(id);
    }

    public async Task<AuthResponse> AuthAsync(string email, string password)
    {
        var model = await GetAsync(x => x.Email.Equals(email) && x.Active);

        if (!BCrypt.Net.BCrypt.Verify(password, model.Password))
            throw new InformationException(Enums.StatusException.IncorrectFormat, "Incorrect username or password");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, model.Profile.Name)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecurityKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new AuthResponse
        {
            Token = tokenString,
            ExpirationDate = tokenDescriptor.Expires
        };
    }
}
