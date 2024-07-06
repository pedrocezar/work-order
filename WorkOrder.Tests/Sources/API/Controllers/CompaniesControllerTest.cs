using WorkOrder.API.Controllers;
using WorkOrder.Domain.Contracts.Requests;
using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Models;
using WorkOrder.Domain.Interfaces.Services;
using WorkOrder.Tests.Configs;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace WorkOrder.Tests.Sources.API.Controllers;

[Trait("Controller", "Controller company")]
public class CompaniesControllerTest
{
    private readonly Mock<ICompanyService> _mockCompanyService;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;
    public CompaniesControllerTest()
    {
        _mockCompanyService = new Mock<ICompanyService>();
        _mapper = MapConfig.Get();
        _fixture = FixtureConfig.Get();
    }

    [Fact(DisplayName = "Search for a company by Id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<CompanyModel>();

        _mockCompanyService.Setup(mock => mock.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

        var controller = new CompaniesController(_mapper, _mockCompanyService.Object);

        var response = await controller.GetByIdAsync(model.Id);

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var empresaResponse = Assert.IsType<CompanyResponse>(objectResult.Value);
        Assert.Equal(empresaResponse.Id, model.Id);
    }

    [Fact(DisplayName = "Search all companies")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<CompanyModel>>();

        _mockCompanyService.Setup(mock => mock.GetAllAsync()).ReturnsAsync(models);

        var controller = new CompaniesController(_mapper, _mockCompanyService.Object);

        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var empresasResponse = Assert.IsType<List<CompanyResponse>>(objectResult.Value);
        Assert.True(empresasResponse.Count > 0);
    }

    [Fact(DisplayName = "Register a new company")]
    public async Task PostAsync()
    {
        var request = _fixture.Create<CompanyRequest>();

        _mockCompanyService.Setup(mock => mock.AddAsync(It.IsAny<CompanyModel>())).Returns(Task.CompletedTask);

        var controller = new CompaniesController(_mapper, _mockCompanyService.Object);

        var response = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(response);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Update an existing company")]
    public async Task PutAsync()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<CompanyRequest>();

        _mockCompanyService.Setup(mock => mock.UpdateAsync(It.IsAny<CompanyModel>())).Returns(Task.CompletedTask);

        var controller = new CompaniesController(_mapper, _mockCompanyService.Object);

        var response = await controller.PutAsync(id, request);

        var objectResult = Assert.IsType<NoContentResult>(response);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [Fact(DisplayName = "Remove an existing company")]
    public async Task DeleteAsync()
    {
        var id = _fixture.Create<int>();

        _mockCompanyService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new CompaniesController(_mapper, _mockCompanyService.Object);

        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);
        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }
}
