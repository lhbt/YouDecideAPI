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
        private const string MongoUrl =
            "mongodb://localhost:27017/YouDecide";

        public GameStateDataAccess()
        {
            var url = new MongoUrl(MongoUrl);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }
        
        public void NewGame(int userId)
        {
            var document = new BsonDocument
            {
                {"_id", userId},
                {"node", "test parent"}
            };

            var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);
            var collection = Database.GetCollection<Thingy>("Thingies");

            // insert object
            collection.InsertOneAsync(new Thingy { Name = "foo" });
            //await collection.FindOneAndReplaceAsync(filter, document);
            //await collection.FindOneAndUpdateAsync(filter, document);

        }
        public List<BsonDocument> GetUser(int userId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", userId);

            var collection = Database.GetCollection<BsonDocument>(CollectionName);

            var test = collection.FindAsync(filter);
            var item = collection.Find(filter).ToListAsync();
            return item.Result;   
        }

    }
    public class Thingy
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    class GameStateDataAccessTests
    {
        [Test]
        public void should_do_something()
        {
            var sut = new GameStateDataAccess();
            sut.NewGame(123);
            var result = sut.GetUser(123);
            Assert.That(true, Is.EqualTo(false));
        }

    }
}
