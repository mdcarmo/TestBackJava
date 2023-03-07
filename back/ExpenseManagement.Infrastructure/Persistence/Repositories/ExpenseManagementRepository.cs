using ExpenseManagement.Core.Entities;
using ExpenseManagement.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;

namespace ExpenseManagement.Infrastructure.Persistence.Repositories
{
    public class ExpenseManagementRepository : IExpenseManagementRepository
    {
        private readonly IMongoCollection<Spent> _collection;

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="database">instância do database</param>
        public ExpenseManagementRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Spent>("spending");
        }

        /// <inheritdoc />
        public async Task AddAsync(Spent spent)
        {
            await _collection.InsertOneAsync(spent);
        }

        /// <inheritdoc />
        public async Task<List<Spent>> GetByCodeAsync(long codeUser)
        {
            return await _collection.Find(c => c.CodeUser == codeUser).ToListAsync();
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
