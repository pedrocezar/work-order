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

[Trait("Controller", "Controller works")]
public class WorksControllerTest
{
    private readonly Mock<IWorkService> _mockWorkService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public WorksControllerTest()
    {
        _mockWorkService = new Mock<IWorkService>();
        _mapper = MapConfig.Get();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Works list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<WorkModel>>();

        _mockWorkService.Setup(mock => mock.GetAllAsync()).ReturnsAsync(models);

        var controller = new WorksController(_mapper, _mockWorkService.Object);

        var actionResult = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<WorkResponse>>(objectResult.Value);
        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search work id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockWorkService.Setup(mock => mock.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

        var controller = new WorksController(_mapper, _mockWorkService.Object);

        var actionResult = await controller.GetByIdAsync(model.Id);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<WorkResponse>(objectResult.Value);
        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register work")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<WorkRequest>();

        _mockWorkService.Setup(mock => mock.AddAsync(It.IsAny<WorkModel>())).Returns(Task.CompletedTask);

        var controller = new WorksController(_mapper, _mockWorkService.Object);

        var actionResult = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Edit existing work")]
    public async Task PutAsync()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<WorkRequest>();

        _mockWorkService.Setup(mock => mock.UpdateAsync(It.IsAny<WorkModel>())).Returns(Task.CompletedTask);

        var controller = new WorksController(_mapper, _mockWorkService.Object);

        var actionResult = await controller.PutAsync(id, request);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Remove existing work")]
    public async Task DeleteAsync()
    {
        var id = _fixture.Create<int>();

        _mockWorkService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new WorksController(_mapper, _mockWorkService.Object);

        var actionResult = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }
}
