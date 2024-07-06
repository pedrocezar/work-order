using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using WorkOrder.Domain.Models;
using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Infrastructure.Repositories;
using WorkOrder.Tests.Configs;
using Xunit;

namespace WorkOrder.Tests.Sources.Infrastructure.Repositories;

[Trait("Repository", "Repository profile")]
public class ProfileRepositoryTest
{
    private readonly Mock<WorkOrderContext> _mockContext;
    private readonly Fixture _fixture;

    public ProfileRepositoryTest()
    {
        _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Profile list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<ProfileModel>>();

        _mockContext.Setup(mock => mock.Set<ProfileModel>()).ReturnsDbSet(models);

        var repository = new ProfileRepository(_mockContext.Object);

        var response = await repository.ListAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search profile id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockContext.Setup(mock => mock.Set<ProfileModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

        var repository = new ProfileRepository(_mockContext.Object);

        var response = await repository.FindAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register profile")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockContext.Setup(mock => mock.Set<ProfileModel>()).ReturnsDbSet(new List<ProfileModel> { new ProfileModel() });

        var repository = new ProfileRepository(_mockContext.Object);

        try
        {
            await repository.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Edit existing profile")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockContext.Setup(mock => mock.Set<ProfileModel>()).ReturnsDbSet(new List<ProfileModel> { new ProfileModel() });

        var repository = new ProfileRepository(_mockContext.Object);

        try
        {
            await repository.EditAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Fact(DisplayName = "Remove existing profile")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockContext.Setup(mock => mock.Set<ProfileModel>()).ReturnsDbSet(new List<ProfileModel> { model });

        var repository = new ProfileRepository(_mockContext.Object);

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
