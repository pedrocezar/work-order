using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class ProfileRepository(WorkOrderContext _workOrderContext) : BaseRepository<ProfileModel>(_workOrderContext), IProfileRepository
{
}
