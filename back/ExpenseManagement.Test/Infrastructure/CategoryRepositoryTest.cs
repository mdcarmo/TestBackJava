using ExpenseManagement.Core.Entities;
using ExpenseManagement.Infrastructure.Persistence.Repositories;
using MongoDB.Driver;

namespace ExpenseManagement.Test.Infrastructure
{
    public  class CategoryRepositoryTest
    {
        private readonly IMongoDatabase _database;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTest()
        {
            // Cria conexão com o MongoDB e obtém o banco de dados de testes
            var client = new MongoClient("mongodb://mdcarmo:teste123@localhost:27017");
            _database = client.GetDatabase("expense-management-db");

            // Cria o repositório a ser testado
            _categoryRepository = new CategoryRepository(_database);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            await SeedDatabase();

            // Act
            var result = await _categoryRepository.GetAll();

            // Assert
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public async Task GetByFilter_ReturnsCategoriesThatMatchSearch()
        {
            // Arrange
            await SeedDatabase();

            // Act
            var result = _categoryRepository.GetByFilter("Lazer");

            // Assert
            Assert.Single(result);
            Assert.Equal("Lazer", result.First().Description);
        }

        private async Task SeedDatabase()
        {
            // Remove todos os documentos da coleção
            await _database.DropCollectionAsync("categories");

            // Insere alguns documentos na coleção
            var categories = new List<Category>
            {
                new Category { Description = "Alimentacao" },
                new Category { Description = "Vestuario" },
                new Category { Description = "Saude" },
                new Category { Description = "Moradia" },
                new Category { Description = "Transporte" },
                new Category { Description = "Lazer" },
                new Category { Description = "Supermercado" }
            };
            await _database.GetCollection<Category>("categories").InsertManyAsync(categories);
        }
    }
}
