using System.Threading.Tasks;
using FavoriteColors.Controllers;
using FavoriteColors.Domain.Models;
using FavoriteColors.Persistence.Contexts;
using FavoriteColors.Persistence.Repositories;
using FavoriteColors.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FavoriteColors.UnitTests
{
    public class PersonsControllerTests
    {
        private readonly Mock<DbSet<Person>> setMock = new();
        private readonly Mock<AppDbContext> contextMock = new();

        [Fact]
        public async Task GetByIdAsync_WithUnexistingId_ReturnsNotFound()
        {
            // Arrange
            contextMock.Setup(m => m.Persons).Returns(setMock.Object);
            var repoMock = new Mock<PersonRepository>(contextMock.Object);
            var personServiceMock = new Mock<PersonService>(repoMock.Object);
            personServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Person)null);

            var controller = new PersonsController(personServiceMock.Object);

            // Act
            var result = await controller.GetByIdAsync(0);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //[Fact]
        //public async Task GetByIdAsync_WithExistingId_ReturnsExpectedPerson()
        //{
        //    // Arrange
        //    var expectedPerson = CreateTestPerson();

        //    var repoMock = new Mock<PersonRepository>(contextMock.Object);
        //    var personServiceMock = new Mock<PersonService>(repoMock.Object);
        //    personServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Person)null);

        //    // Act

        //    // Assert
        //}

        private Person CreateTestPerson()
        {
            return new()
            {
                Id = 1,
                Name = "Max",
                LastName = "Mustermann",
                ZipCode = 12345,
                City = "Musterstadt",
                Color = ColorEnum.blau
            };
        }
    }
}
