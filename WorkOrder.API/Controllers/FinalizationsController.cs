using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using static WorkOrder.Domain.Utils.ConstantUtil;

namespace WorkOrder.API.Controllers;

[Authorize(Roles = ServiceProviderProfileName)]
public class FinalizationsController(IMapper _mapper, IFinalizationService _service) : BaseController<FinalizationModel, FinalizationRequest, FinalizationResponse> (_mapper, _service)
{
}
