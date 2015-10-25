using System.Collections.Generic;
using System.Threading.Tasks;
using YouDecide.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YouDecide.Mongo
{
    using System.Linq;

    public class MongoDataAccess : IDataAccess
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;

        private const string StoryCollectionName = "MasterStory_mongo";
        private const string GameCollectionName = "gameStates";

        private const string MongoUrl = "mongodb://localhost:27017/test";
        //private const string MongoUrl = "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

        public MongoDataAccess()
        {
            var url = new MongoUrl(MongoUrl);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
        }

        public async Task<List<StoryPoint>> FetchAllStoryPoints()
        {
            var retrievedData = new List<StoryPoint>();

            var collection = Database.GetCollection<BsonDocument>(StoryCollectionName);
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

        public Task UpdateGameParents(List<StoryPoint> parents, string gameId)
        {

            var filter = Builders<GameState>.Filter.Eq("GameId",gameId);
            var update = Builders<GameState>.Update.Set("Parents", parents);
            return Database.GetCollection<GameState>(GameCollectionName).UpdateOneAsync(filter, update);
        }

        public Task UpdateGamePoints(List<StoryPoint> points, string gameId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("GameId", gameId);
            var update = Builders<BsonDocument>.Update.Set("Points", points);
            return Database.GetCollection<BsonDocument>(GameCollectionName).UpdateOneAsync(filter, update);
        }

        public async Task<List<StoryPoint>> GetGameStoryPoints(string gameId)
        {
            var filter = Builders<GameState>.Filter.Eq("GameId", gameId);
            var results = await Database.GetCollection<GameState>(GameCollectionName).Find(filter).ToListAsync();
            var returnObject = results.First();
            return returnObject.Points;
        }

        public async Task<List<StoryPoint>> GetGameStoryParents(string gameId)
        {
            var filter = Builders<GameState>.Filter.Eq("GameId", gameId);
            var results = await Database.GetCollection<GameState>(GameCollectionName).Find(filter).ToListAsync();
            var returnObject = results.First();
            return returnObject.Parents;
        }

        public async Task<List<BsonDocument>> TestFilterData()
        {
            var collection = Database.GetCollection<BsonDocument>(StoryCollectionName);
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

            var collection = Database.GetCollection<BsonDocument>(StoryCollectionName);
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateGameState(int gameId)
        {
            var gameState = new GameState
            {
                GameId = 1234,
                GameOptions = new List<GameOption>
                        {
                            new GameOption
                                {
                                    Option = "foo option",
                                    OptionNumber = 1
                                },
                            new GameOption
                                {
                                    Option = "bar option",
                                    OptionNumber = 2
                                },
                        },
                History = "NOFING M8"
            };

            await Database.GetCollection<GameState>(GameCollectionName).InsertOneAsync(gameState);
        }

        public Task<List<GameState>> GetGameState(int gameId)
        {
            return Database.GetCollection<GameState>(GameCollectionName).Find((state => state.GameId == gameId)).ToListAsync();
        }

    }
}
