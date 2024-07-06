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

[Trait("Controller", "Controller finalizations")]
public class FinalizacaosControllerTest
{
    private readonly Mock<IFinalizationService> _mockFinalizationService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public FinalizacaosControllerTest()
    {
        _mockFinalizationService = new Mock<IFinalizationService>();
        _mapper = MapConfig.Get();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "List of finalization")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<FinalizationModel>>();

        _mockFinalizationService.Setup(mock => mock.GetAllAsync()).ReturnsAsync(models);

        var controller = new FinalizationsController(_mapper, _mockFinalizationService.Object);

        var actionResult = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<FinalizationResponse>>(objectResult.Value);
        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search finalization id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockFinalizationService.Setup(mock => mock.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

        var controller = new FinalizationsController(_mapper, _mockFinalizationService.Object);

        var actionResult = await controller.GetByIdAsync(model.Id);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<FinalizationResponse>(objectResult.Value);
        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Registration finalization")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<FinalizationRequest>();

        _mockFinalizationService.Setup(mock => mock.AddAsync(It.IsAny<FinalizationModel>())).Returns(Task.CompletedTask);

        var controller = new FinalizationsController(_mapper, _mockFinalizationService.Object);

        var actionResult = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Edit existing finalization")]
    public async Task PutAsync()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<FinalizationRequest>();

        _mockFinalizationService.Setup(mock => mock.UpdateAsync(It.IsAny<FinalizationModel>())).Returns(Task.CompletedTask);

        var controller = new FinalizationsController(_mapper, _mockFinalizationService.Object);

        var actionResult = await controller.PutAsync(id, request);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Remove existing finalization")]
    public async Task DeleteAsync()
    {
        var id = _fixture.Create<int>();

        _mockFinalizationService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new FinalizationsController(_mapper, _mockFinalizationService.Object);

        var actionResult = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }
}
