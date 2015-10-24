using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using YouDecide.Domain;

namespace YouDecide.Mongo
{
    using NUnit.Framework;

    class GameStateDataAccess
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;
        private const string CollectionName = "User_mongo";
        private const string MongoUrlLocal = "mongodb://localhost:27017/test";
        private const string MongoUrl =
            "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

        public GameStateDataAccess()
        {
            var url = new MongoUrl(MongoUrl);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }

        public async void NewGame(string userId)
        {
            var document = new BsonDocument
            {
                {"_id", userId},
                {"parent", "test parent"},
                {"child", "test child"}
            };

            var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);
            var collection = Database.GetCollection<dynamic>("Users");

            // insert object
            await collection.InsertOneAsync(document);
            //await collection.FindOneAndReplaceAsync(filter, document);
            //await collection.FindOneAndUpdateAsync(filter, document);

        }
        public Task<List<BsonDocument>> GetUser(string userId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);

            var collection = Database.GetCollection<BsonDocument>(CollectionName);

            return collection.FindAsync(filter).Result.ToListAsync();
        }

    }

    [TestFixture]
    class GameStateDataAccessTests
    {
        [Test]
        public void should_do_something()
        {
            var sut = new GameStateDataAccess();
            sut.NewGame("123");
            var result = sut.GetUser("123");
            Assert.That(true, Is.EqualTo(false));
        }

    }
}
