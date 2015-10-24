using System.Collections.Generic;
using System.Threading.Tasks;

using YouDecide.Domain;

using MongoDB.Driver;

namespace YouDecide.Mongo
{
    public class GameStateDataAccess
    {
        protected static IMongoClient Client;
        protected static IMongoDatabase Database;
        protected static IMongoCollection<GameState> Collection; 

        private const string CollectionName = "gameStates";
        private const string MongoUrlLocal = "mongodb://localhost:27017/test";
        private const string MongoUrl =
            "mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m";

        public GameStateDataAccess()
        {
            var url = new MongoUrl(MongoUrlLocal);
            var client = new MongoClient(url);
            Database = client.GetDatabase(url.DatabaseName);
            Collection = Database.GetCollection<GameState>(CollectionName);
        }

        public async Task NewGame(int gameId)
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

            await Collection.InsertOneAsync(gameState);
        }

        public Task<List<GameState>> GetUser(int gameId)
        {
            return Collection.Find((state => state.GameId == gameId)).ToListAsync();
        }

    }
}
