using System;
using MongoDB.Driver;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using DanceBotDb.Configuration;
using Akka.Configuration;
using MongoDB.Bson;
using DanceBotShared.Db.Messages.Queries;
using DanceBotShared.Db.Messages.Results;
using DanceBotShared.Db.Messages.Models;

namespace DanceBotDb.Common
{
	public class DbContext : IDbContext
    {
        private readonly IMongoDatabase db;

        public DbContext(IDbConfiguration configuration)
		{
            var client = new MongoClient(configuration.ConnectionString);
            db = client.GetDatabase(configuration.DbName);
        }

        public async Task<IList<T>> GetAll<T>() where T : Document
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById<T>(string id) where T : Document
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            return await collection.Find<T>(filter).FirstOrDefaultAsync();
        }

        public async Task<T> Add<T>(T entity) where T : Document
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var objectId = ObjectId.GenerateNewId().ToString();
            entity.Id = objectId;
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> Replace<T>(string id, T entity) where T : Document
        {
            var collection = db.GetCollection<T>(typeof(T).Name);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            await collection.ReplaceOneAsync(filter, entity);
            return entity;
        }
    }
}

