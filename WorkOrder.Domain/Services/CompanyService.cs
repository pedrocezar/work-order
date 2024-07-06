using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace WorkOrder.Domain.Services;

public class CompanyService(ICompanyRepository _repository, IHttpContextAccessor _httpContextAccessor) : BaseService<CompanyModel>(_repository, _httpContextAccessor), ICompanyService
{
}
