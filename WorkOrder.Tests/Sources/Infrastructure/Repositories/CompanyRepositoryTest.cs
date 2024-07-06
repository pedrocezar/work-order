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

[Trait("Repository", "Repository company")]
public class CompanyRepositoryTest
{
    private readonly Fixture _fixture;
    private readonly Mock<WorkOrderContext> _mockContext;

    public CompanyRepositoryTest()
    {
        _fixture = FixtureConfig.Get();
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
    }

    [Fact(DisplayName = "List all companies")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<CompanyModel>>();

        _mockContext.Setup(mock => mock.Set<CompanyModel>()).ReturnsDbSet(models);

        var repository = new CompanyRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "List company by id")]
    public async Task GetByIdAsync()
    {
        var models = _fixture.Create<CompanyModel>();

        _mockContext.Setup(mock => mock.Set<CompanyModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(models);

        var repository = new CompanyRepository(_mockContext.Object);

        var id = models.Id;
        var response = await repository.FindAsync(id);

        Assert.Equal(response.Id, id);
    }

    [Fact(DisplayName = "Register a new company")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockContext.Setup(mock => mock.Set<CompanyModel>()).ReturnsDbSet(new List<CompanyModel>());

        var repository = new CompanyRepository(_mockContext.Object);

        var exception = await Record.ExceptionAsync(() => repository.AddAsync(model));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Change an existing company")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockContext.Setup(mock => mock.Set<CompanyModel>()).ReturnsDbSet(new List<CompanyModel>());

        var repository = new CompanyRepository(_mockContext.Object);

        var exception = await Record.ExceptionAsync(() => repository.EditAsync(model));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Delete an existing company")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockContext.Setup(mock => mock.Set<CompanyModel>()).ReturnsDbSet(new List<CompanyModel>());

        var repository = new CompanyRepository(_mockContext.Object);

        var exception = await Record.ExceptionAsync(() => repository.RemoveAsync(model));
        Assert.Null(exception);
    }
}
