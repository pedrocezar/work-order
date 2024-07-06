using WorkOrder.API.Controllers;
using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace WorkOrder.Tests.Sources.API.Controllers;

[Trait("Controller", "Controller users")]
public class AuthsControllerTest
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Fixture _fixture;

    public AuthsControllerTest()
    {
        _mockUserService = new Mock<IUserService>();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Create token")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<AuthRequest>();
        var response = _fixture.Create<AuthResponse>();

        _mockUserService.Setup(mock => mock.AuthAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(response);

        var controller = new AuthsController(_mockUserService.Object);

        var actionResult = await controller.PostAsync(request);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var responseResult = Assert.IsType<AuthResponse>(objectResult.Value);
        Assert.Equal(responseResult.Token, response.Token);
    }
}
