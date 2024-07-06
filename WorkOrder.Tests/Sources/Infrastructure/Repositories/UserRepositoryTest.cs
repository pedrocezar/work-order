using WorkOrder.Domain.Models;
using WorkOrder.Infrastructure.Contexts;
using WorkOrder.Infrastructure.Repositories;
using WorkOrder.Tests.Configs;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace WorkOrder.Tests.Sources.Infrastructure.Repositories
{
    [Trait("Repository", "Repository user")]
    public class UserRepositoryTest
    {
        private readonly Mock<WorkOrderContext> _mockContext;
        private readonly Fixture _fixture;

        public UserRepositoryTest()
        {
            _mockContext = new Mock<WorkOrderContext>(new DbContextOptionsBuilder<WorkOrderContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "User list")]
        public async Task GetAsync()
        {
            var models = _fixture.Create<List<UserModel>>();

            _mockContext.Setup(mock => mock.Set<UserModel>()).ReturnsDbSet(models);

            var repository = new UserRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count > 0);
        }

        [Fact(DisplayName = "Search user id")]
        public async Task GetByIdAsync()
        {
            var model = _fixture.Create<UserModel>();

            _mockContext.Setup(mock => mock.Set<UserModel>().FindAsync(It.IsAny<int>())).ReturnsAsync(model);

            var repository = new UserRepository(_mockContext.Object);

            var response = await repository.FindAsync(model.Id);

            Assert.Equal(response.Id, model.Id);
        }

        [Fact(DisplayName = "Register user")]
        public async Task AddAsync()
        {
            var model = _fixture.Create<UserModel>();

            _mockContext.Setup(mock => mock.Set<UserModel>()).ReturnsDbSet(new List<UserModel> { new UserModel() });

            var repository = new UserRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(model);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edit existing user")]
        public async Task UpdateAsync()
        {
            var model = _fixture.Create<UserModel>();

            _mockContext.Setup(mock => mock.Set<UserModel>()).ReturnsDbSet(new List<UserModel> { new UserModel() });

            var repository = new UserRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(model);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove existing user")]
        public async Task DeleteAsync()
        {
            var model = _fixture.Create<UserModel>();

            _mockContext.Setup(mock => mock.Set<UserModel>()).ReturnsDbSet(new List<UserModel> { model });

            var repository = new UserRepository(_mockContext.Object);

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
}
