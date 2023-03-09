using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace ExpenseManagement.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _collection;

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="database">instância do database</param>
        public CategoryRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Category>("categories");
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetAll()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        /// <inheritdoc />
        public List<Category> GetByFilter(string search)
        {
            var filter = new BsonDocument { { "Description", new BsonDocument { { "$regex", search }, { "$options", "i" } } } };
            return _collection.Find(filter).ToList();
        }
    }
}
