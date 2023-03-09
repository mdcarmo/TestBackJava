using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;
using System.Linq;

namespace ExpenseManagement.Infrastructure.Persistence.Repositories
{
    public class ExpenseManagementRepository : IExpenseManagementRepository
    {
        private readonly IMongoCollection<Spent> _collection;
        private readonly IMongoCollection<Category> _collectionCategory;

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="database">instância do database</param>
        public ExpenseManagementRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Spent>("spending");
            _collectionCategory = database.GetCollection<Category>("categories");
        }

        /// <inheritdoc />
        public async Task AddAsync(Spent spent)
        {
            await _collection.InsertOneAsync(spent);
            
            //verifica se a categoria já existe na coleção  
            await CheckIfCategoryExists(spent);
        }

        private async Task CheckIfCategoryExists(Spent spent)
        {
            if (!string.IsNullOrEmpty(spent.Category))
            {
                var categoryDb = await _collectionCategory.Find(c => c.Description.Contains(spent.Category)).FirstOrDefaultAsync();
                if (categoryDb == null)
                {
                    Category category = new Category();
                    category.Description = spent.Category;

                    await _collectionCategory.InsertOneAsync(category);
                }
            }
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(string id, Spent spent)
        {
            ReplaceOneResult result = await _collection.ReplaceOneAsync(x => x.Id == id, spent);
            
            if(result.ModifiedCount > 0)
            {
                await CheckIfCategoryExists(spent);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<List<Spent>> GetByCodeAsync(long codeUser)
        {
            return await _collection.Find(c => c.CodeUser == codeUser).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Spent> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            return await _collection.Find($"{{ _id: ObjectId('{objectId}') }}").SingleAsync();
        }

        /// <inheritdoc />
        public async Task<List<Spent>> GetByCodeAndDateAsync(long codeUser, DateTime dateFind)
        {
            DateTime fromDate = Convert.ToDateTime(dateFind.ToString("yyyy-dd-MM'T'00:00:01.fffffff'Z'"), CultureInfo.InvariantCulture);
            DateTime toDate = Convert.ToDateTime(dateFind.ToString("yyyy-dd-MM'T'23:59:59.fffffff'Z'"), CultureInfo.InvariantCulture);

            var dataquery = new BsonDocument
            {
                {"PostedAt", new BsonDocument
                {
                    { "$gt", fromDate },
                    { "$lt", toDate }
                } }
            };

            return await _collection.Find(dataquery).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Spent> GetCategoryContentDescriptionAsync(long codeUser, string description)
        {
            return await _collection.Find(c => c.CodeUser == codeUser 
                                           && c.Description.Contains(description)).FirstOrDefaultAsync();
        }





    }
}
