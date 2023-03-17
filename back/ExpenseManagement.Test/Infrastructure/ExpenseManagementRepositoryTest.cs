using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using ExpenseManagement.Infrastructure.Persistence.Repositories;
using MongoDB.Driver;

namespace ExpenseManagement.Test.Infrastructure
{
    public class ExpenseManagementRepositoryTest
    {
        private IMongoDatabase _database;
        private IExpenseManagementRepository _expenseManagementRepository;
       
        public ExpenseManagementRepositoryTest()
        {
            // Configuração do banco de dados MongoDB em memória para os testes
            var client = new MongoClient("mongodb://mdcarmo:teste123@localhost:27017");
            _database = client.GetDatabase("expense-management-db");

            // Instanciação do ExpenseManagementRepository para os testes
            _expenseManagementRepository = new ExpenseManagementRepository(_database);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewExpenseAndCategory()
        {
            // Arrange
            var spent = new Spent
            {
                Description = "Compra no mercado",
                Value = 50.00,
                Category = "Alimentacao",
                CodeUser = 123
            };

            await _database.DropCollectionAsync("spending");
            await _database.DropCollectionAsync("categories");

            // Act
            await _expenseManagementRepository.AddAsync(spent);

            // Assert
            var expenses = await _expenseManagementRepository.GetByCodeAsync(123);
            var categories = await _database.GetCollection<Category>("categories").Find(x => true).ToListAsync();

            Assert.True(expenses.Count > 0);
            Assert.True(categories.Count > 0);
            Assert.Equal(expenses[0].Description, spent.Description);
            Assert.Equal(categories[0].Description, spent.Category);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExpenseAndAddNewCategory()
        {
            // Arrange
            var spent = new Spent
            {
                Description = "Compra no mercado da esquina",
                Value = 50.00,
                Category = "Supermercado",
                CodeUser = 123
            };

            await _database.DropCollectionAsync("spending");
            await _database.DropCollectionAsync("categories");

            await _expenseManagementRepository.AddAsync(spent);
            
            var expenses = await _expenseManagementRepository.GetByCodeAsync(123);
  
            spent.Value = 80.00;
            spent.Category = "Supermercado";

            // Act
            var result = await _expenseManagementRepository.UpdateAsync(expenses.FirstOrDefault().Id, spent);
            var categories = await _database.GetCollection<Category>("categories").Find(x => true).ToListAsync();

            // Assert
            Assert.True(result);
            Assert.Equal(expenses[0].Description, spent.Description);
            Assert.Equal(categories[0].Description, spent.Category);
        }

        [Fact]
        public async Task GetByCodeAsync_ShouldReturnExpensesByCode()
        {
            // Arrange
            var spent1 = new Spent
            {
                Description = "Compra no mercado",
                Value = 50.00,
                Category = "Alimentação",
                CodeUser = 123
            };

            var spent2 = new Spent
            {
                Description = "Conta de luz",
                Value = 100.00,
                Category = "Contas",
                CodeUser = 123
            };

            await _database.DropCollectionAsync("spending");
            await _database.DropCollectionAsync("categories");

            await _expenseManagementRepository.AddAsync(spent1);
            await _expenseManagementRepository.AddAsync(spent2);

            // Act
            var result = await _expenseManagementRepository.GetByCodeAsync(123);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnExpenseById()
        {
            // Arrange
            var spent = new Spent
            {
                Description = "Compra no mercado",
                Value = 50.00,
                Category = "Alimentação",
                CodeUser = 123
            };
            
            await _database.DropCollectionAsync("spending");
            await _database.DropCollectionAsync("categories");

            await _expenseManagementRepository.AddAsync(spent);

            var expenses = await _expenseManagementRepository.GetByCodeAsync(123);

            // Act
            var result = await _expenseManagementRepository.GetByIdAsync(expenses.FirstOrDefault().Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, expenses.FirstOrDefault().Id);
        }


        [Fact]
        public async Task GetCategoryContentDescriptionAsync_ShouldReturnSpentWithMatchingDescription()
        {
            // Arrange
            long codeUser = 123;
            string description = "Compra no mercado";
            
            var expectedSpent = new Spent
            {
                Description = "Compra no mercado",
                Value = 50.00,
                Category = "Alimentação",
                CodeUser = 123
            };

            await _database.DropCollectionAsync("spending");
            await _database.DropCollectionAsync("categories");

            await _expenseManagementRepository.AddAsync(expectedSpent);

            // Act
            var actualSpent = await _expenseManagementRepository.GetCategoryContentDescriptionAsync(codeUser, description);

            // Assert
            Assert.NotNull(actualSpent);
        }
    }
}
