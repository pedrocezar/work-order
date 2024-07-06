using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace WorkOrder.Domain.Services;

public class ProfileService(IProfileRepository _repository, IHttpContextAccessor _httpContextAccessor) : BaseService<ProfileModel>(_repository, _httpContextAccessor), IProfileService
{
}
