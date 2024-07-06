using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class RelationalRepository(WorkOrderContext _workOrderContext) : BaseRepository<RelationalModel>(_workOrderContext), IRelationalRepository
{
}
