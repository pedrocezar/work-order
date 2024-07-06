using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Interfaces.Services;

namespace WorkOrder.API.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
[ProducesResponseType(typeof(InformationResponse), 400)]
[ProducesResponseType(typeof(InformationResponse), 401)]
[ProducesResponseType(typeof(InformationResponse), 403)]
[ProducesResponseType(typeof(InformationResponse), 404)]
[ProducesResponseType(typeof(InformationResponse), 500)]
public class AuthsController(IUserService _userService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<ActionResult<AuthResponse>> PostAsync([FromBody] AuthRequest request)
    {
        var response = await _userService.AuthAsync(request.Email, request.Password);
        return Ok(response);
    }
}
