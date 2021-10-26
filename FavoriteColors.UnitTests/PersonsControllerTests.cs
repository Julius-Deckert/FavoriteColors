using System;
using System.Threading.Tasks;
using FavoriteColors.Controllers;
using FavoriteColors.Domain.Models;
using FavoriteColors.Dtos;
using FavoriteColors.Persistence.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FavoriteColors.UnitTests
{
    public class PersonsControllerTests
    {
        private readonly Random rand = new();
        private readonly Array colors = Enum.GetValues(typeof(Color));
        private readonly Mock<PersonRepository> repoMock = new();

        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Person)null);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.GetByIdAsync(rand.Next());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            var expectedItem = CreateRandomPerson();

            repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedItem);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.GetByIdAsync(expectedItem.Id);

            // Assert
            result.Value.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        public async Task GetItemsAsync_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomPerson(), CreateRandomPerson(), CreateRandomPerson() };

            repoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedItems);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var actualItems = await controller.GetAllAsync();

            // Assert
            actualItems.Should().BeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var itemToCreate = new CreatePersonDto(
                rand.Next(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.CreatePersonAsync(itemToCreate);

            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as PersonDto;
            itemToCreate.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<PersonDto>().ExcludingMissingMembers()
            );
        }

        private Person CreateRandomPerson()
        {
            return new()
            {
                Id = rand.Next(),
                Name = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                ZipCode = 12345,
                City = GenerateRandomString(8),
                Color = colors.GetValue(rand.Next(colors.Length)).ToString()
            };
        }

        private string GenerateRandomString(int length)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            var Charsarr = new char[length];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            return new string(Charsarr);
        }
    }
}
