using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FavoriteColors.Repositories
{
    public class MongoDbPersonsRepository : IPersonRepository
    {
        private const string databaseName = "FavoriteColors";
        private const string collectionName = "persons";
        private readonly IMongoCollection<Person> itemsCollection;
        private readonly FilterDefinitionBuilder<Person> filterBuilder = Builders<Person>.Filter;

        public MongoDbPersonsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Person>(collectionName);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            var filter = filterBuilder.Eq(person => person.Color, color.ToString());
            return await itemsCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Person person)
        {
            await itemsCollection.InsertOneAsync(person);
        }
    }
}