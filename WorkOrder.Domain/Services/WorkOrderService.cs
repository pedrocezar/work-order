using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace WorkOrder.Domain.Services;

public class WorkOrderService(IWorkOrderRepository _repository, IHttpContextAccessor _httpContextAccessor) : BaseService<WorkOrderModel>(_repository, _httpContextAccessor), IWorkOrderService
{
}
