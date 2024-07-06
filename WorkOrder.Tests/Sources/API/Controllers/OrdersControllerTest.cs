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

[Trait("Controller", "Controller orders")]
public class OrdersControllerTest
{
    private readonly Mock<IWorkOrderService> _mockWorkOrderService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public OrdersControllerTest()
    {
        _mockWorkOrderService = new Mock<IWorkOrderService>();
        _mapper = MapConfig.Get();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Work order list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<WorkOrderModel>>();

        _mockWorkOrderService.Setup(mock => mock.GetAllAsync()).ReturnsAsync(models);

        var controller = new OrdersController(_mapper, _mockWorkOrderService.Object);

        var actionResult = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<List<WorkOrderResponse>>(objectResult.Value);
        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search work order id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<WorkOrderModel>();

        _mockWorkOrderService.Setup(mock => mock.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

        var controller = new OrdersController(_mapper, _mockWorkOrderService.Object);

        var actionResult = await controller.GetByIdAsync(model.Id);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var response = Assert.IsType<WorkOrderResponse>(objectResult.Value);
        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register work order")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<WorkOrderRequest>();

        _mockWorkOrderService.Setup(mock => mock.AddAsync(It.IsAny<WorkOrderModel>())).Returns(Task.CompletedTask);

        var controller = new OrdersController(_mapper, _mockWorkOrderService.Object);

        var actionResult = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(actionResult);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Edit existing work order")]
    public async Task PutAsync()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<WorkOrderRequest>();

        _mockWorkOrderService.Setup(mock => mock.UpdateAsync(It.IsAny<WorkOrderModel>())).Returns(Task.CompletedTask);

        var controller = new OrdersController(_mapper, _mockWorkOrderService.Object);

        var actionResult = await controller.PutAsync(id, request);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Remove existing work order")]
    public async Task DeleteAsync()
    {
        var id = _fixture.Create<int>();

        _mockWorkOrderService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new OrdersController(_mapper, _mockWorkOrderService.Object);

        var actionResult = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(actionResult);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }
}
