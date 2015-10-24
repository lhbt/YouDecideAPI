﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public sealed class GameState
    {
        public string History { get; set; }
        public List<GameOption> GameOptions { get; set; }
    }
}
