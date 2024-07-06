using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Domain.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkOrder.API.Controllers;

[Authorize(Roles = ConstantUtil.ServiceProviderProfileName)]
public class CompaniesController : BaseController<CompanyModel, CompanyRequest, CompanyResponse>
{
    private readonly IMapper _mapper;
    private readonly ICompanyService _service;

    public CompaniesController(IMapper mapper, ICompanyService service) : base(mapper, service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(201)]
    public override async Task<ActionResult> PostAsync([FromBody] CompanyRequest request)
    {
        var model = _mapper.Map<CompanyModel>(request);
        await _service.AddAsync(model);
        return Created(nameof(PostAsync), new { id = model.Id });
    }
}
