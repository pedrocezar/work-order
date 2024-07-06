using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WorkOrder.API.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(InformationResponse), 400)]
[ProducesResponseType(typeof(InformationResponse), 401)]
[ProducesResponseType(typeof(InformationResponse), 403)]
[ProducesResponseType(typeof(InformationResponse), 404)]
[ProducesResponseType(typeof(InformationResponse), 500)]
public class BaseController<TModel, KRequest, YResponse>(IMapper _mapper, IBaseService<TModel> _service) : ControllerBase where TModel : BaseModel
{
    [HttpPost]
    [ProducesResponseType(201)]
    public virtual async Task<ActionResult> PostAsync([FromBody] KRequest request)
    {
        var model = _mapper.Map<TModel>(request);
        await _service.AddAsync(model);
        return Created(nameof(PostAsync), new { id = model.Id });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public virtual async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] KRequest request)
    {
        var model = _mapper.Map<TModel>(request);
        model.Id = id;
        await _service.UpdateAsync(model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public virtual async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet()]
    [ProducesResponseType(200)]
    public virtual async Task<ActionResult<List<YResponse>>> GetAsync()
    {
        var models = await _service.GetAllAsync();
        var response = _mapper.Map<List<YResponse>>(models);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public virtual async Task<ActionResult<YResponse>> GetByIdAsync([FromRoute] int id)
    {
        var model = await _service.GetByIdAsync(id);
        var response = _mapper.Map<YResponse>(model);
        return Ok(response);
    }
}