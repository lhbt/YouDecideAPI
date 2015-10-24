using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace YouDecide.Domain
{
    public sealed class GameState
    {
        public ObjectId Id { get; set; }
        public string History { get; set; }
        public List<GameOption> GameOptions { get; set; }

        public int GameId { get; set; }
    }
}
