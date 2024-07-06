using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;

namespace WorkOrder.Infrastructure.Repositories;

public class CompanyRepository(WorkOrderContext _workOrderContext) : BaseRepository<CompanyModel>(_workOrderContext), ICompanyRepository
{
}
