using ExpenseManagement.Api.Application.Services;
using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using Moq;

namespace ExpenseManagement.Test.Application
{
    public class CategoryServiceTest
    {
        /// <summary>
        /// Mock para o repositório de categorias.
        /// </summary>
        private Mock<ICategoryRepository> _mockRepository;
        private readonly CategoryService _service;

        public CategoryServiceTest()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfCategories()
        {
            // Arrange
            var expectedCategories = new List<Category>
            {
                new Category { Id = "1", Description = "Medicamento" },
                new Category { Id = "2", Description = "Diversão" },
                new Category { Id = "3", Description = "Escola" }
            };

            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedCategories);
            
            // Act
            var actualCategories = await _service.GetAll();

            // Assert
            Assert.Equal(expectedCategories, actualCategories);
        }

        [Fact]
        public void GetByFilter_ReturnsListOfCategories()
        {
            // Arrange
            var search = "Category";
            var expectedCategories = new List<Category>
            {
                new Category { Id = "1", Description = "Category 1" },
                new Category { Id = "2", Description = "Category 2" },
                new Category { Id = "3", Description = "Category 3" }
            };

            _mockRepository.Setup(repo => repo.GetByFilter(search)).Returns(expectedCategories);

            // Act
            var actualCategories = _service.GetByFilter(search);

            // Assert
            Assert.Equal(expectedCategories, actualCategories);
        }
    }
}
