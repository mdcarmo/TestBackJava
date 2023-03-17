using ExpenseManagement.Api.Application.Dto;
using ExpenseManagement.Api.Application.Services;
using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using Moq;

namespace ExpenseManagement.Test.Application
{
    public class ExpenseManagementServiceTest
    {
        private readonly Mock<IExpenseManagementRepository> _mockRepository;
        private readonly ExpenseManagementService _service;

        public ExpenseManagementServiceTest()
        {
            _mockRepository = new Mock<IExpenseManagementRepository>();
            _service = new ExpenseManagementService(_mockRepository.Object);
        }

        [Fact]
        public async Task AddAsync_WithNullCategory_FindsCategoryAndAddsSpent()
        {
            // Arrange
            var dto = new SpentDto(123, "Remedio para dor", 100, DateTime.Now, "");
            var expectedId = Guid.NewGuid().ToString();

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Spent>()))
                .Callback<Spent>(spent => spent.Id = expectedId)
                .Returns(Task.CompletedTask);

            _mockRepository.Setup(repo => repo.GetCategoryContentDescriptionAsync(dto.CodeUser, dto.Description))
                .ReturnsAsync(new Spent { Category = "Medicacao" });

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.Equal(expectedId, result);
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Spent>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WithValidCategory_AddsSpent()
        {
            // Arrange
            var dto = new SpentDto(123,"Almoço de domingo",250.0,DateTime.Now,"Alimentacao");
            var expectedId = Guid.NewGuid().ToString();

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Spent>()))
                .Callback<Spent>(spent => spent.Id = expectedId)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddAsync(dto);

            // Assert
            Assert.Equal(expectedId, result);
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Spent>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithValidId_UpdatesSpent()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var dto = new SpentDto(123, "Almoço de domingo", 260.0, DateTime.Now, "Alimentacao");

            _mockRepository.Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(new Spent { Id = id, CodeUser = 123, Description = "Almoço de domingo", Value = 260.0, Category = "Alimentacao" });

            _mockRepository.Setup(repo => repo.UpdateAsync(id, It.IsAny<Spent>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, dto);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.UpdateAsync(id, It.IsAny<Spent>()), Times.Once);
        }

        [Fact]
        public async Task GetByCodeAsync_ReturnsListOfSpentDto()
        {
            // Arrange
            long codeUser = 123;
            List<Spent> spendingList = new List<Spent>()
            {
                new Spent() { Id = "1", CodeUser = 123, Description = "Almoço de sabado", Value = 250.0, Category = "Alimentacao" },
                new Spent() { Id = "1", CodeUser = 123, Description = "Almoço de domingo", Value = 260.0, Category = "Alimentacao" }
            };

            _mockRepository.Setup(repo => repo.GetByCodeAsync(codeUser)).ReturnsAsync(spendingList);
           
            // Act
            var result = await _service.GetByCodeAsync(codeUser);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SpentDto>>(result);
            Assert.Equal(spendingList.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsSpentDto()
        {
            // Arrange
            string validId = "123";
            var validSpent = new Spent() { Id = "1", CodeUser = 123, Description = "Almoço de sabado", Value = 250.0, Category = "Alimentacao" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(validSpent);

            // Act
            var result = await _service.GetByIdAsync(validId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SpentDto>(result);
            Assert.Equal(validSpent.Id, result.Id);
            Assert.Equal(validSpent.Description, result.Description);
        }

        [Fact]
        public async Task GetByCodeAndDateAsync_WithValidParams_ReturnsListSpentDto()
        {
            // Arrange
            long validCode = 123;
            DateTime validDate = new DateTime(2023, 3, 16);
            var validSpendings = new List<Spent>()
            {
                new Spent() { Id = "1", CodeUser = validCode, Description = "Spending 1", Value = 10, PostedAt = validDate },
                new Spent() { Id = "2", CodeUser = validCode, Description = "Spending 2", Value = 20, PostedAt = validDate },
                new Spent() { Id = "3", CodeUser = validCode, Description = "Spending 3", Value = 30, PostedAt = validDate }
            };

            _mockRepository.Setup(repo => repo.GetByCodeAndDateAsync(validCode, validDate)).ReturnsAsync(validSpendings);

            // Act
            var result = await _service.GetByCodeAndDateAsync(validCode, validDate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SpentDto>>(result);
            Assert.Equal(validSpendings.Count, result.Count);

            for (int i = 0; i < validSpendings.Count; i++)
            {
                Assert.Equal(validSpendings[i].Id, result[i].Id);
                Assert.Equal(validSpendings[i].Description, result[i].Description);
                Assert.Equal(validSpendings[i].Value, result[i].Value);
                Assert.Equal(validSpendings[i].CodeUser, result[i].CodeUser);
                Assert.Equal(validSpendings[i].PostedAt, result[i].PostedAt);
            }
        }

        [Fact]
        public async Task GetByCodeAndDateAsync_WithInvalidParams_ReturnsEmptyList()
        {
            // Arrange
            long invalidCode = -1;
            DateTime invalidDate = new DateTime(2023, 3, 16);

            _mockRepository.Setup(repo => repo.GetByCodeAndDateAsync(invalidCode, invalidDate)).ReturnsAsync(new List<Spent>());

            // Act
            var result = await _service.GetByCodeAndDateAsync(invalidCode, invalidDate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SpentDto>>(result);
            Assert.Empty(result);
        }
    }
}
