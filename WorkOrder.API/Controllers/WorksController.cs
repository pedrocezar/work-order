using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using static WorkOrder.Domain.Utils.ConstantUtil;

namespace WorkOrder.API.Controllers;

[Authorize(Roles = ServiceProviderProfileName)]
public class WorksController(IMapper _mapper, IWorkService _service) : BaseController<WorkModel, WorkRequest, WorkResponse>(_mapper, _service)
{
}
