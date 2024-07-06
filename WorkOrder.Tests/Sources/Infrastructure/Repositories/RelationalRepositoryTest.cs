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

[Trait("Repository", "Repository relational")]
public class RelationalRepositoryTest
{
    private readonly Mock<WorkOrderContext> _mockContext;
    private readonly Fixture _fixture;

    public RelationalRepositoryTest()
    {
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Relational list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<RelationalModel>>();

        _mockContext.Setup(mock => mock.Set<RelationalModel>()).ReturnsDbSet(models);

        var repository = new RelationalRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search relational id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<RelationalModel>();

        _mockContext.Setup(mock => mock.Set<RelationalModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

        var repository = new RelationalRepository(_mockContext.Object);

        var response = await repository.FindAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register relational")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<RelationalModel>();

        _mockContext.Setup(mock => mock.Set<RelationalModel>()).ReturnsDbSet(new List<RelationalModel> { new RelationalModel() });

        var repository = new RelationalRepository(_mockContext.Object);

        try
        {
            await repository.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Edit existing relational")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<RelationalModel>();

        _mockContext.Setup(mock => mock.Set<RelationalModel>()).ReturnsDbSet(new List<RelationalModel> { new RelationalModel() });

        var repository = new RelationalRepository(_mockContext.Object);

        try
        {
            await repository.EditAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing relational")]
    public async Task DeleteAsync()
    {
        var entity = _fixture.Create<RelationalModel>();

        _mockContext.Setup(mock => mock.Set<RelationalModel>()).ReturnsDbSet(new List<RelationalModel> { entity });

        var repository = new RelationalRepository(_mockContext.Object);

        try
        {
            await repository.RemoveAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }
}
