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

namespace WorkOrder.Tests.Sources.API.Controllers
{
    [Trait("Controller", "Controller users")]
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public UsersControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "User list")]
        public async Task GetAsync()
        {
            var models = _fixture.Create<List<UserModel>>();

            _mockUserService.Setup(mock => mock.GetAllUsersAsync()).ReturnsAsync(models);

            var controller = new UsersController(_mapper, _mockUserService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<UserResponse>>(objectResult.Value);
            Assert.True(response.Count > 0);
        }

        [Fact(DisplayName = "Search user id")]
        public async Task GetByIdAsync()
        {
            var model = _fixture.Create<UserModel>();

            _mockUserService.Setup(mock => mock.GetByIdUserAsync(It.IsAny<int>())).ReturnsAsync(model);

            var controller = new UsersController(_mapper, _mockUserService.Object);

            var actionResult = await controller.GetByIdAsync(model.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<UserResponse>(objectResult.Value);
            Assert.Equal(response.Id, model.Id);
        }

        [Fact(DisplayName = "Register user")]
        public async Task PostAsync()
        {
            var request = _fixture.Create<UserRequest>();

            _mockUserService.Setup(mock => mock.AddAsync(It.IsAny<UserModel>())).Returns(Task.CompletedTask);

            var controller = new UsersController(_mapper, _mockUserService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edit existing user")]
        public async Task PutAsync()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<UserRequest>();

            _mockUserService.Setup(mock => mock.UpdateAsync(It.IsAny<UserModel>())).Returns(Task.CompletedTask);

            var controller = new UsersController(_mapper, _mockUserService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove existing user")]
        public async Task DeleteAsync()
        {
            var id = _fixture.Create<int>();

            _mockUserService.Setup(mock => mock.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new UsersController(_mapper, _mockUserService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
