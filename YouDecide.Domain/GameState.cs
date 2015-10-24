using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace YouDecide.Domain
{
    public class GameState
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        
        public string History { get; set; }
        
        public List<GameOption> GameOptions { get; set; }

        public int GameId { get; set; }
    }
}
