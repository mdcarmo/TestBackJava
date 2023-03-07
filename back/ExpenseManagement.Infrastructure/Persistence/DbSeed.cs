using ExpenseManagement.Core.Entities;
using MongoDB.Driver;

namespace ExpenseManagement.Infrastructure.Persistence
{
    public class DbSeed
    {
        private readonly IMongoCollection<Spent> _collection;
        
        private List<Spent> _spentList = new List<Spent> {
            new Spent(1234, "Restaurante XPTO", 285, DateTime.Now.Date, "Alimentacao"),
            new Spent(1234, "Supermercado WWWW", 485, DateTime.Now.Date.AddHours(2), "Despesas Mensais"),
            new Spent(1234, "Despesa diversa", 485, DateTime.Now.Date.AddHours(3), "Moradia"),
            new Spent(1234, "Transporte Escolar", 180, DateTime.Now.Date.AddHours(3), "Transporte")
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
