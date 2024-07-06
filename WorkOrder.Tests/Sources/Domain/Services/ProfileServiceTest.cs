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

[Trait("Service", "Service profile")]
public class ProfileServiceTest
{
    private readonly Mock<IProfileRepository> _mockProfileRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Claim[] _claims;

    public ProfileServiceTest()
    {
        _mockProfileRepository = new Mock<IProfileRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _claims = _fixture.Create<UserModel>().Claims();
    }

    [Fact(DisplayName = "Profile list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<ProfileModel>>();

        _mockProfileRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<ProfileModel, bool>>>())).ReturnsAsync(models);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new ProfileService(_mockProfileRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetAllAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search profile id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<ProfileModel>();

        _mockProfileRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProfileModel, bool>>>())).ReturnsAsync(model);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new ProfileService(_mockProfileRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetByIdAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register profile")]
    public async Task AddAsync()
    {
        var entity = _fixture.Create<ProfileModel>();

        _mockProfileRepository.Setup(mock => mock.AddAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new ProfileService(_mockProfileRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AddAsync(entity);
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

        _mockProfileRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ProfileModel, bool>>>())).ReturnsAsync(model);
        _mockProfileRepository.Setup(mock => mock.EditAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new ProfileService(_mockProfileRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.UpdateAsync(model);
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

        _mockProfileRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(model);
        _mockProfileRepository.Setup(mock => mock.RemoveAsync(It.IsAny<ProfileModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new ProfileService(_mockProfileRepository.Object, _mockHttpContextAccessor.Object);

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
