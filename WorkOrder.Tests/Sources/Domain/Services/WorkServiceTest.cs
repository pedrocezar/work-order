using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Services;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace WorkOrder.Tests.Sources.Domain.Services;

[Trait("Service", "Service work")]
public class WorkServiceTest
{
    private readonly Mock<IWorkRepository> _mockWorkRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Claim[] _claims;

    public WorkServiceTest()
    {
        _mockWorkRepository = new Mock<IWorkRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _claims = _fixture.Create<UserModel>().Claims();
    }

    [Fact(DisplayName = "Work list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<WorkModel>>();

        _mockWorkRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<WorkModel, bool>>>())).ReturnsAsync(models);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new WorkService(_mockWorkRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetAllAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search work id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockWorkRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<WorkModel, bool>>>())).ReturnsAsync(model);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new WorkService(_mockWorkRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetByIdAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register work")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockWorkRepository.Setup(mock => mock.AddAsync(It.IsAny<WorkModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new WorkService(_mockWorkRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Edit existing work")]
    public async Task UpdateAsync()
     {
        var model = _fixture.Create<WorkModel>();

        _mockWorkRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<WorkModel, bool>>>())).ReturnsAsync(model);
        _mockWorkRepository.Setup(mock => mock.EditAsync(It.IsAny<WorkModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new WorkService(_mockWorkRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.UpdateAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing work")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockWorkRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(model);
        _mockWorkRepository.Setup(mock => mock.RemoveAsync(It.IsAny<WorkModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new WorkService(_mockWorkRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.DeleteAsync(model.Id);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }
}
