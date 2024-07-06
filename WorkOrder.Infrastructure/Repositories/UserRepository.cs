using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class UserRepository(WorkOrderContext _workOrderContext) : BaseRepository<UserModel>(_workOrderContext), IUserRepository
{
}
