using System.Collections.Generic;
using MongoDB.Driver.Builders;
using YouDecide.Domain;
using MongoDB.Driver;

namespace YouDecide.Mongo
{
    using System.Linq;

    public class MongoDataAccess : IDataAccess
    {
        protected static MongoClient Client;
        protected static MongoDatabase Database;

        private const string StoryCollectionName = "MasterStory_mongo";
        private const string GameCollectionName = "gameStates";

        private const string LocalMongoUrl = "mongodb://localhost:27017/test";
        private const string MongoUrl = "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

        public MongoDataAccess()
        {
            var url = new MongoUrl(MongoUrl);
            var client = new MongoClient(url);
            Database = client.GetServer().GetDatabase(url.DatabaseName);
        }

        public List<StoryPoint> FetchAllStoryPoints()
        {
            var collection = Database.GetCollection<StoryPoint>(StoryCollectionName);

            var stories = collection.FindAll();

            return stories.ToList();
        }

        public void CreateGameState(GameState gameState)
        {
            Database.GetCollection<GameState>(GameCollectionName).Insert(gameState);
        }

        public void UpdateGameState(GameState gameState)
        {
            var query = Query<GameState>.EQ(x => x.GameId, gameState.GameId);
            Database.GetCollection<GameState>(GameCollectionName).Update(query, Update.Replace(gameState));
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
            var query = Query<GameState>.EQ(x => x.GameId, gameId);
            var gameState = Database.GetCollection<GameState>(GameCollectionName).Find(query).FirstOrDefault();
            return gameState ?? new GameState { GameId = gameId, DeathlyDeathText = "", History = "" };
        }

        public void DeleteGameState(string gameId)
        {
            var query = Query<GameState>.EQ(x => x.GameId, gameId); 
            Database.GetCollection<GameState>(GameCollectionName).Remove(query);
        }
    }
}
