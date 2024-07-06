using WorkOrder.Domain.Models;
using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Infrastructure.Repositories;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace WorkOrder.Tests.Sources.Infrastructure.Repositories;

[Trait("Repository", "Repository work order")]
public class WorkOrderRepositoryTest
{
    private readonly Mock<WorkOrderContext> _mockContext;
    private readonly Fixture _fixture;

    public WorkOrderRepositoryTest()
    {
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Work order list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<WorkOrderModel>>();

        _mockContext.Setup(mock => mock.Set<WorkOrderModel>()).ReturnsDbSet(models);

        var repository = new WorkOrderRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search work order Id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<WorkOrderModel>();

        _mockContext.Setup(mock => mock.Set<WorkOrderModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

        var repository = new WorkOrderRepository(_mockContext.Object);

        var response = await repository.FindAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register work order")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<WorkOrderModel>();

        _mockContext.Setup(mock => mock.Set<WorkOrderModel>()).ReturnsDbSet(new List<WorkOrderModel> { new WorkOrderModel() });

        var repository = new WorkOrderRepository(_mockContext.Object);

        try
        {
            await repository.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Edit existing work order")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<WorkOrderModel>();

        _mockContext.Setup(mock => mock.Set<WorkOrderModel>()).ReturnsDbSet(new List<WorkOrderModel> { new WorkOrderModel() });

        var repository = new WorkOrderRepository(_mockContext.Object);

        try
        {
            await repository.EditAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing work order")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<WorkOrderModel>();

        _mockContext.Setup(mock => mock.Set<WorkOrderModel>()).ReturnsDbSet(new List<WorkOrderModel> { model });

        var repository = new WorkOrderRepository(_mockContext.Object);

        try
        {
            await repository.RemoveAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }
}
