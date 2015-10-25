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
     
        public string DeathlyDeathText { get; set; }

        public string GameId { get; set; }

        public string Gif { get; set; }

        public List<GameOption> GameOptions { get; set; }

        public List<StoryPoint> Points { get; set; }

        public List<StoryPoint> Parents { get; set; }
    }
}
