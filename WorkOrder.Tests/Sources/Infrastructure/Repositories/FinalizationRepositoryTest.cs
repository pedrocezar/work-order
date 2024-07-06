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

[Trait("Repository", "Repository finalization")]
public class FinalizationRepositoryTest
{
    private readonly Mock<WorkOrderContext> _mockContext;
    private readonly Fixture _fixture;

    public FinalizationRepositoryTest()
    {
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Finalization list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<FinalizationModel>>();

        _mockContext.Setup(mock => mock.Set<FinalizationModel>()).ReturnsDbSet(models);

        var repository = new FinalizationRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search finalization id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockContext.Setup(mock => mock.Set<FinalizationModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

        var repository = new FinalizationRepository(_mockContext.Object);

        var response = await repository.FindAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Registration finalization")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockContext.Setup(mock => mock.Set<FinalizationModel>()).ReturnsDbSet(new List<FinalizationModel> { new FinalizationModel() });

        var repository = new FinalizationRepository(_mockContext.Object);

        try
        {
            await repository.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Edit existing finalization")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockContext.Setup(mock => mock.Set<FinalizationModel>()).ReturnsDbSet(new List<FinalizationModel> { new FinalizationModel() });

        var repository = new FinalizationRepository(_mockContext.Object);

        try
        {
            await repository.EditAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing finalization")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockContext.Setup(mock => mock.Set<FinalizationModel>()).ReturnsDbSet(new List<FinalizationModel> { model });

        var repository = new FinalizationRepository(_mockContext.Object);

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
