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

        //private const string MongoUrl = "mongodb://localhost:27017/test";
        private const string MongoUrl = "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

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

        public async Task UpdateGameState(GameState gameState)
        {
            await Database.GetCollection<GameState>(GameCollectionName).InsertOneAsync(gameState);
        }

        public async Task UpdateGameParents(List<StoryPoint> parents, string gameId)
        {
            var gameState = GetCurrentGameState(gameId);

            gameState.Parents = parents;
            
            await Database.GetCollection<GameState>(GameCollectionName).ReplaceOneAsync(x => x.GameId == gameId, gameState);
        }

        public async Task UpdateGamePoints(List<StoryPoint> points, string gameId)
        {
            var gameState = GetCurrentGameState(gameId);

            gameState.Points = points;

            await Database.GetCollection<GameState>(GameCollectionName).ReplaceOneAsync(x => x.GameId == gameId, gameState);
        }

        public List<StoryPoint> GetGameStoryPoints(string gameId)
        {
            var gameState = GetCurrentGameState(gameId);
            return gameState.Points;
        }

        public List<StoryPoint> GetGameStoryParents(string gameId)
        {
            var gameState = GetCurrentGameState(gameId);
            return gameState.Parents;
        }

        public GameState GetCurrentGameState(string gameId)
        {
            var gameState =
                Database.GetCollection<GameState>(GameCollectionName)
                        .Find((state => state.GameId == gameId)).FirstOrDefaultAsync().Result;

            return gameState ?? new GameState { GameId = gameId, DeathlyDeathText = "", History = "" };
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
    }
}
