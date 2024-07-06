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

[Trait("Repository", "Repository work")]
public class WorkRepositoryTest
{
    private readonly Mock<WorkOrderContext> _mockContext;
    private readonly Fixture _fixture;

    public WorkRepositoryTest()
    {
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Work list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<WorkModel>>();

        _mockContext.Setup(mock => mock.Set<WorkModel>()).ReturnsDbSet(models);

        var repository = new WorkRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search work id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockContext.Setup(mock => mock.Set<WorkModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

        var repository = new WorkRepository(_mockContext.Object);

        var response = await repository.FindAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register work")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockContext.Setup(mock => mock.Set<WorkModel>()).ReturnsDbSet(new List<WorkModel> { new WorkModel() });

        var repository = new WorkRepository(_mockContext.Object);

        try
        {
            await repository.AddAsync(model);
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

        _mockContext.Setup(mock => mock.Set<WorkModel>()).ReturnsDbSet(new List<WorkModel> { new WorkModel() });

        var repository = new WorkRepository(_mockContext.Object);

        try
        {
            await repository.EditAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing model")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<WorkModel>();

        _mockContext.Setup(mock => mock.Set<WorkModel>()).ReturnsDbSet(new List<WorkModel> { model });

        var repository = new WorkRepository(_mockContext.Object);

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
