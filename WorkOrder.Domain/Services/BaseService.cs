using WorkOrder.Domain.Models;
using WorkOrder.Domain.Exceptions;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace WorkOrder.Domain.Services;

public class BaseService<T> : IBaseService<T> where T : BaseModel
{
    private readonly IBaseRepository<T> _repository;
    public readonly int? UserId;
    public readonly string UserPerfil;

    public BaseService(IBaseRepository<T> repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        UserId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
        UserPerfil = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _repository.ListAsync(x => x.Active);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
    {
        return await _repository.ListAsync(expression);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await _repository.FindAsync(expression);
        if (entity == null)
            throw new InformationException(Enums.StatusException.NotFound, $"No data found");

        return entity;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _repository.FindAsync(x => x.Id == id && x.Active);
        if (entity == null)
            throw new InformationException(Enums.StatusException.NotFound, $"No data found for Id {id}");

        return entity;
    }

    public async Task AddAsync(T model)
    {
        model.CreatedAt = DateTime.Now;
        model.CreatedBy = UserId;
        await _repository.AddAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _repository.FindAsync(id);
        if (model == null)
            throw new InformationException(Enums.StatusException.NotFound, $"No data found for Id {id}");

        model.UpdatedBy = UserId;
        model.DataAlteracao = DateTime.Now;
        model.Active = false;
        await _repository.EditAsync(model);
    }

    public async Task UpdateAsync(T model)
    {
        var find = await _repository.FindAsNoTrackingAsync(x => x.Id == model.Id && x.Active);
        if (find == null)
            throw new InformationException(Enums.StatusException.NotFound, $"No data found for Id {model.Id}");

        model.CreatedAt = find.CreatedAt;
        model.DataAlteracao = DateTime.Now;
        model.UpdatedBy = UserId;
        await _repository.EditAsync(model);
    }
}
