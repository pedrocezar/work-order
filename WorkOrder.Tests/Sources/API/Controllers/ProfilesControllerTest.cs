using WorkOrder.API.Controllers;
using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Tests.Configs;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace WorkOrder.Tests.Sources.API.Controllers;

[Trait("Controller", "Controller profiles")]
public class ProfilesControllerTest
{
    private readonly Mock<IProfileService> _mockProfileService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public ProfilesControllerTest()
    {
        _mockProfileService = new Mock<IProfileService>();
        _mapper = MapConfig.Get();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Profile list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<ProfileModel>>();

        _mockProfileService.Setup(mock => mock.GetAllAsync()).ReturnsAsync(models);

        var controller = new ProfilesController(_mapper, _mockProfileService.Object);

        var actionResult = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<ProfileResponse>>(objectResult.Value);
        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search profile id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockProfileService.Setup(mock => mock.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

        var controller = new ProfilesController(_mapper, _mockProfileService.Object);

        var actionResult = await controller.GetByIdAsync(model.Id);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<ProfileResponse>(objectResult.Value);
        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register profile")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<ProfileRequest>();

        _mockProfileService.Setup(mock => mock.AddAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);

        var controller = new ProfilesController(_mapper, _mockProfileService.Object);

        var actionResult = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Edit existing profile")]
    public async Task PutAsync()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<ProfileRequest>();

        _mockProfileService.Setup(mock => mock.UpdateAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);

        var controller = new ProfilesController(_mapper, _mockProfileService.Object);

        var actionResult = await controller.PutAsync(id, request);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Remove existing profile")]
    public async Task DeleteAsync()
    {
        var id = _fixture.Create<int>();

        _mockProfileService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new ProfilesController(_mapper, _mockProfileService.Object);

        var actionResult = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }
}
