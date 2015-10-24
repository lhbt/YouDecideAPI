using System.Collections.Generic;
using System.Threading.Tasks;
using YouDecide.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YouDecide.Mongo
{
    public class MongoDataAccess : IDataAccess
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;
        private const string CollectionName = "MasterStory_mongo";
        private const string MongoUrlLocal = "mongodb://localhost:27017/test";
        private const string MongoUrl =
            "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

        public MongoDataAccess()
        {
            var url = new MongoUrl(MongoUrlLocal);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }

        public async Task<List<StoryPoint>> FetchAllStoryPoints()
        {
            var retrievedData = new List<StoryPoint>();

            //IMongoCollection<StoryPoint> collection = Database.GetCollection<StoryPoint>(CollectionName);

            IMongoCollection<BsonDocument> collection = Database.GetCollection<BsonDocument>(CollectionName);
            var filter = new BsonDocument();
            IFindFluent<BsonDocument, BsonDocument> cursor2 = collection.Find(filter);

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
            var collection = Database.GetCollection<BsonDocument>(CollectionName);
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

            var collection = Database.GetCollection<BsonDocument>(CollectionName);
            await collection.InsertOneAsync(document);
        }
    }
}
