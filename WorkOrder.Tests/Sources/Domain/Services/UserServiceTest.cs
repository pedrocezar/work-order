using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Services;
using WorkOrder.Domain.Settings;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Bogus;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace WorkOrder.Tests.Sources.Domain.Services;

[Trait("Service", "Service user")]
public class UserServiceTest
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<AppSetting> _mockAppSetting;
    private readonly Faker _faker;
    private readonly Fixture _fixture;

    public UserServiceTest()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _faker = new Faker();
        _fixture = FixtureConfig.Get();
        _mockAppSetting = new Mock<AppSetting>();
    }

    [Theory(DisplayName = "User list")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task GetAsync(string perfil)
    {
        var models = _fixture.Create<List<UserModel>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);
        
        _mockUserRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<UserModel, bool>>>())).ReturnsAsync(models);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UserService(_mockUserRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetAllAsync();

        Assert.True(response.Count > 0);
    }

    [Theory(DisplayName = "Search user id")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task GetByIdAsync(string perfil)
    {
        var model = _fixture.Create<UserModel>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUserRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UserModel, bool>>>())).ReturnsAsync(model);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UserService(_mockUserRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetByIdAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Register user")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<UserModel>();

        _mockUserRepository.Setup(mock => mock.AddAsync(It.IsAny<UserModel>())).Returns(Task.CompletedTask);

        var service = new UserService(_mockUserRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AddAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Theory(DisplayName = "Edit existing user")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task UpdateAsync(string perfil)
     {
        var model = _fixture.Create<UserModel>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUserRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UserModel, bool>>>())).ReturnsAsync(model);
        _mockUserRepository.Setup(mock => mock.EditAsync(It.IsAny<UserModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UserService(_mockUserRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.UpdateAsync(model);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }

    [Theory(DisplayName = "Remove existing user")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task DeleteAsync(string perfil)
    {
        var model = _fixture.Create<UserModel>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUserRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(model);
        _mockUserRepository.Setup(mock => mock.RemoveAsync(It.IsAny<UserModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UserService(_mockUserRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

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
