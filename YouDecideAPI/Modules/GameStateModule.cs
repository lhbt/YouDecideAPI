﻿using Nancy;
using Newtonsoft.Json;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class GameStateModule : NancyModule
    {
        public GameStateModule(IStoryNavigator storyNavigator)
        {
            var gameState = storyNavigator.GetCurrentGameState("0");

            Get["/gamestate"] = parameters => JsonConvert.SerializeObject(gameState);
        }
    }
}