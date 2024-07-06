using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class WorkOrderRepository(WorkOrderContext _workOrderContext) : BaseRepository<WorkOrderModel>(_workOrderContext), IWorkOrderRepository
{
}
