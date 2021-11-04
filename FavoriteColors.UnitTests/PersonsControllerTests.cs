using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Controllers;
using FavoriteColors.Dtos;
using FavoriteColors.Models;
using FavoriteColors.Repositories;
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
        private readonly Mock<IPersonRepository> repoMock = new();

        [Fact]
        public async Task GetAllAsync_WithExistingPersons_ReturnsAllIPersons()
        {
            // Arrange
            var expectedPersons = new[] { CreateRandomPerson(), CreateRandomPerson(), CreateRandomPerson() };

            repoMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedPersons);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var actualPersons = await controller.GetAllAsync();

            // Assert
            actualPersons.Should().BeEquivalentTo(expectedPersons);
        }

        [Fact]
        public async Task GetByIdAsync_WithUnexistingPerson_ReturnsBadRequest()
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
        public async Task GetByIdAsync_WithExistingPerson_ReturnsExpectedPerson()
        {
            // Arrange
            var expectedPerson = CreateRandomPerson();

            repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedPerson);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.GetByIdAsync(expectedPerson.Id);

            // Assert
            result.Value.Should().BeEquivalentTo(expectedPerson);
        }

        [Fact]
        public async Task GetByColorAsync_WithNonMatchingPersons_ReturnsNothing()
        {
            // Arrange
            List<Person> personList = new();
            var nonMatchingPerson = new Person()
            {
                Id = rand.Next(),
                Name = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                ZipCode = rand.Next(11111, 99999),
                City = GenerateRandomString(8),
                Color = Color.blau.ToString()
            };
            personList.Add(nonMatchingPerson);

            repoMock.Setup(repo => repo.GetByColorAsync(It.IsAny<Color>()))
                .ReturnsAsync(personList);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.GetByColorAsync(Color.violett);

            // Assert
            result.Value.Should().BeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetByColorAsync_WithMatchingPerson_ReturnsExpectedPersons()
        {
            // Arrange
            List<Person> personList = new();
            var matchingPerson = new Person()
            {
                Id = rand.Next(),
                Name = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                ZipCode = rand.Next(11111, 99999),
                City = GenerateRandomString(8),
                Color = Color.blau.ToString()
            };
            personList.Add(matchingPerson);

            repoMock.Setup(repo => repo.GetByColorAsync(It.IsAny<Color>()))
                .ReturnsAsync(personList);

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.GetByColorAsync(Color.blau);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreatePersonAsync_WithPersonToCreate_ReturnsCreatedPerson()
        {
            // Arrange
            var personToCreate = new CreatePersonDto(
                rand.Next(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var controller = new PersonsController(repoMock.Object);

            // Act
            var result = await controller.CreatePersonAsync(personToCreate);

            // Assert
            var createdPerson = (result as CreatedAtActionResult).Value as PersonDto;
            personToCreate.Should().BeEquivalentTo(
                createdPerson,
                options => options.ComparingByMembers<PersonDto>().ExcludingMissingMembers()
            );
        }

        [Fact]
        public async Task CreatePersonAsync_WithExistingId_ReturnsBadRequest()
        {
            // Arrange
            var existingPerson = CreateRandomPerson();

            var personToCreate = new CreatePersonDto(
                existingPerson.Id,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var controller = new PersonsController(repoMock.Object);

            repoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingPerson);

            // Act
            var result = await controller.CreatePersonAsync(personToCreate);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        private Person CreateRandomPerson()
        {
            return new()
            {
                Id = rand.Next(),
                Name = GenerateRandomString(8),
                LastName = GenerateRandomString(8),
                ZipCode = rand.Next(11111, 99999),
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
