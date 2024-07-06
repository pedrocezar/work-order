﻿using WorkOrder.Domain.Models;
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

[Trait("Service", "Service finalization")]
public class FinalizationServiceTest
{
    private readonly Mock<IFinalizationRepository> _mockFinalizationRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Claim[] _claims;

    public FinalizationServiceTest()
    {
        _mockFinalizationRepository = new Mock<IFinalizationRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _claims = _fixture.Create<UserModel>().Claims();
    }

    [Fact(DisplayName = "Finalization list")]
    public async Task GetAsync()
    {
        var models = _fixture.Create<List<FinalizationModel>>();

        _mockFinalizationRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<FinalizationModel, bool>>>())).ReturnsAsync(models);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new FinalizationService(_mockFinalizationRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetAllAsync();

        Assert.True(response.Count > 0);
    }

    [Fact(DisplayName = "Search finalization id")]
    public async Task GetByIdAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockFinalizationRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<FinalizationModel, bool>>>())).ReturnsAsync(model);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new FinalizationService(_mockFinalizationRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.GetByIdAsync(model.Id);

        Assert.Equal(response.Id, model.Id);
    }

    [Fact(DisplayName = "Registration finalization")]
    public async Task AddAsync()
    {
        var model = _fixture.Create<FinalizationModel>();

        _mockFinalizationRepository.Setup(mock => mock.AddAsync(It.IsAny<FinalizationModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new FinalizationService(_mockFinalizationRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AddAsync(model);
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

        _mockFinalizationRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<FinalizationModel, bool>>>())).ReturnsAsync(model);
        _mockFinalizationRepository.Setup(mock => mock.EditAsync(It.IsAny<FinalizationModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new FinalizationService(_mockFinalizationRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.UpdateAsync(model);
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

        _mockFinalizationRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(model);
        _mockFinalizationRepository.Setup(mock => mock.RemoveAsync(It.IsAny<FinalizationModel>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

        var service = new FinalizationService(_mockFinalizationRepository.Object, _mockHttpContextAccessor.Object);

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
