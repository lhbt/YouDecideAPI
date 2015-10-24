using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using YouDecide.Domain;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace YouDecide.Mongo
{
    public class MongoDataAccess : IDataAccess
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;

        public MongoDataAccess()
        {
            Client = new MongoClient();
            Database = Client.GetDatabase("test");
        }

        public async Task<List<StoryPoint>> FetchAllStoryPoints()
        {
            var retrievedData = new List<StoryPoint>();

            var collection = Database.GetCollection<BsonDocument>("youdecide");
            var filter = new BsonDocument();

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument document in batch)
                    {
                        retrievedData.Add(document.ConvertToStoryPoint());
                    }
                }
            }

            return retrievedData;
        }

        public async Task<List<BsonDocument>> TestFilterData()
        {
            var collection = Database.GetCollection<BsonDocument>("youdecide");
            var filter = Builders<BsonDocument>.Filter.Eq("child", "You turn left.");
            List<BsonDocument> result = await collection.Find(filter).ToListAsync();

            return result;
        }

        public async void InsertTestData()
        {
            var document = new BsonDocument
            {
                {"_id", "99999"},
                {"parent", "test parent"},
                {"child", "test child"}
            };

            var collection = Database.GetCollection<BsonDocument>("youdecide");
            await collection.InsertOneAsync(document);
        }
    }
}
