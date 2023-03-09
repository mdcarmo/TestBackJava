using ExpenseManagement.Core.Entities;
using MongoDB.Driver;

namespace ExpenseManagement.Infrastructure.Persistence
{
    public class DbSeed
    {
        private readonly IMongoCollection<Spent> _collection;

        private List<Spent> _spentList = new List<Spent> {
            new Spent(){ CodeUser=1234, Description="Restaurante XPTO", Value=285, PostedAt=DateTime.Now.Date, Category="Alimentacao" },
            new Spent(){ CodeUser=1234, Description="Supermercado WWWW", Value=485, PostedAt=DateTime.Now.Date.AddHours(2), Category="Despesas Mensais" },
            new Spent(){ CodeUser=1234, Description="Despesa diversa", Value=485, PostedAt=DateTime.Now.Date.AddHours(3), Category="Moradia" },
            new Spent(){ CodeUser=1234, Description="Transporte Escolar", Value=180, PostedAt=DateTime.Now.Date.AddHours(3), Category="Transporte" }
        };

        public DbSeed(IMongoDatabase database)
        {
            _collection = database.GetCollection<Spent>("spending");
        }

        public void Populate()
        {
            if (_collection.CountDocuments(c => true) == 0)
                _collection.InsertMany(_spentList);
        }
    }
}
