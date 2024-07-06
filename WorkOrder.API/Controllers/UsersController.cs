using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Domain.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkOrder.API.Controllers;

[Authorize(Roles = ConstantUtil.ProfilesName)]
public class UsersController : BaseController<UserModel, UserRequest, UserResponse>
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UsersController(IMapper mapper, IUserService service) : base(mapper, service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(201)]
    public override async Task<ActionResult> PostAsync([FromBody] UserRequest request)
    {
        var model = _mapper.Map<UserModel>(request);
        await _service.AddUserAsync(model);
        return Created(nameof(PostAsync), new { id = model.Id });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> PatchAsync([FromRoute] int id, [FromBody] PhoneUserRequest request)
    {
        await _service.UpdatePhoneNumberAsync(id, request.PhoneNumber);
        return Ok();
    }

    [HttpGet("nome")]
    [ProducesResponseType(200)]
    protected async Task<ActionResult<List<UserResponse>>> GetAsync([FromQuery] string nome)
    {
        var models = await _service.GetAllAsync(x => x.Name.Contains(nome));
        var response = _mapper.Map<List<UserResponse>>(models);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public override async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] UserRequest request)
    {
        var model = _mapper.Map<UserModel>(request);
        model.Id = id;
        await _service.UpdateUserAsync(model);
        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public override async Task<ActionResult<UserResponse>> GetByIdAsync([FromRoute] int id)
    {
        var entity = await _service.GetByIdUserAsync(id);
        return Ok(_mapper.Map<UserResponse>(entity));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public override async Task<ActionResult<List<UserResponse>>> GetAsync()
    {
        var entity = await _service.GetAllUsersAsync();
        return Ok(_mapper.Map<List<UserResponse>>(entity));
    }
}
