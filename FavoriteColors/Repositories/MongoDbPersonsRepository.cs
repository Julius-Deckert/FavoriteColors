﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FavoriteColors.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FavoriteColors.Repositories
{
    public class MongoDbPersonsRepository : IPersonRepository
    {
        private const string DatabaseName = "FavoriteColors";
        private const string CollectionName = "persons";
        private readonly IMongoCollection<Person> _itemsCollection;
        private readonly FilterDefinitionBuilder<Person> _filterBuilder = Builders<Person>.Filter;

        public MongoDbPersonsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            _itemsCollection = database.GetCollection<Person>(CollectionName);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var filter = _filterBuilder.Eq(person => person.Id, id);
            return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetByColorAsync(Color color)
        {
            var filter = _filterBuilder.Eq(person => person.Color, color.ToString());
            return await _itemsCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Person person)
        {
            await _itemsCollection.InsertOneAsync(person);
        }
    }
}