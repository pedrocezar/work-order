using WorkOrder.Domain.Models;
using WorkOrder.Domain.Exceptions;
using WorkOrder.Domain.Interfaces.Repositories;
using WorkOrder.Domain.Services;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Bogus;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace WorkOrder.Tests.Sources.Domain.Services;

[Trait("Service", "Service company")]
public class CompanyServiceTest
{
    private readonly Mock<ICompanyRepository> _mockCompanyRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Faker _faker;

    public CompanyServiceTest()
    {
        _mockCompanyRepository = new Mock<ICompanyRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }

    [Theory(DisplayName = "Search for a company by id")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task GetByIdAsync(string profile)
    {
        var model = _fixture.Create<CompanyModel>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, profile);

        _mockCompanyRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<CompanyModel, bool>>>())).ReturnsAsync(model);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetByIdAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Theory(DisplayName = "Search for a company by non-existing id")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task GetByIdErrorAsync(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockCompanyRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<CompanyModel, bool>>>())).ReturnsAsync((CompanyModel)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformationException>(() => service.GetByIdAsync(id));
    }

    [Theory(DisplayName = "Search all companies")]
    [InlineData("Client")]
    [InlineData("ServiceProvider")]
    public async Task GetAsync(string perfil)
    {
        var models = _fixture.Create<List<CompanyModel>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockCompanyRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<CompanyModel, bool>>>())).ReturnsAsync(models);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var response = await service.GetAllAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Register a new company")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockCompanyRepository.Setup(mock => mock.AddAsync(It.IsAny<CompanyModel>())).Returns(Task.CompletedTask);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AddAsync(model));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Update an existing company")]
    public async Task UpdateAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockCompanyRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<CompanyModel, bool>>>())).ReturnsAsync(model);
        _mockCompanyRepository.Setup(mock => mock.EditAsync(It.IsAny<CompanyModel>())).Returns(Task.CompletedTask);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.UpdateAsync(model));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Remove an existing company")]
    public async Task DeleteAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockCompanyRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(model);
        _mockCompanyRepository.Setup(mock => mock.RemoveAsync(It.IsAny<CompanyModel>())).Returns(Task.CompletedTask);

        var service = new CompanyService(_mockCompanyRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.DeleteAsync(model.Id));
        Assert.Null(exception);
    }
}
