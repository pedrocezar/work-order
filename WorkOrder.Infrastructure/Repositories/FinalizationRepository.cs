using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class FinalizationRepository(WorkOrderContext _workOrderContext) : BaseRepository<FinalizationModel>(_workOrderContext), IFinalizationRepository
{
}
