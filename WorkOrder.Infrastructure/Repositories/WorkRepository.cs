using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class WorkRepository(WorkOrderContext _workOrderContext) : BaseRepository<WorkModel>(_workOrderContext), IWorkRepository
{
}
