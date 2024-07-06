using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;

namespace WorkOrder.Domain.Interfaces.Services;

public interface IUserService : IBaseService<UserModel>
{
    Task AddUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel user);
    Task<AuthResponse> AuthAsync(string email, string password);
    Task UpdatePhoneNumberAsync(int id, string phoneNumber);
    Task<List<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetByIdUserAsync(int id);
}
