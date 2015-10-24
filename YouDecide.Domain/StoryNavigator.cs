﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public sealed class StoryNavigator : IStoryNavigator
    {
        private readonly IDataAccess _dataAccessor;
        private readonly Dictionary<string, ProcessSMSCommand> _smsCommandProcessors;
        private delegate Task<GameState> ProcessSMSCommand(string smsCommand);

        static List<StoryPoint> _storyTree;
        static List<StoryPoint> _currentStoryParents;
        static List<StoryPoint> _currentStoryPoints;
        private static GameState _currentGameState;

        public StoryNavigator(IDataAccess dataAccessor)
        {
            _dataAccessor = dataAccessor;
            _smsCommandProcessors
                = new Dictionary<string, ProcessSMSCommand>
                    {
                        {"START", StartGame},
                        {"BACK", GetPreviousOptions}
                    };
        }

        public GameState GetCurrentGameState()
        {
            return _currentGameState;
        }

        private async Task<GameState> StartGame(string smsCommand)
        {
            _storyTree = await Initialise();

            _currentStoryParents.Add(_storyTree.First(x => x.Parent == "nothing"));

            LoadOptions();

            return UpdateAndReturnCurrentGameState();
        }

        public async Task<GameState> ProcessSMSInputReturningGameState(string smsMessage)
        {
            if (smsMessage.All(Char.IsDigit))
            {
                _currentGameState = GetNextOptions(int.Parse(smsMessage));
            }
            else
            {
                _currentGameState = await _smsCommandProcessors[smsMessage.ToUpper()](smsMessage);
            }

            return _currentGameState;
        }

        public async Task<string> ProcessSMSInput(string smsMessage)
        {
            string response = "Thank you for your input: " + smsMessage;

            if (smsMessage.All(Char.IsDigit))
            {
                _currentGameState = GetNextOptions(int.Parse(smsMessage));
            }
            else
            {
                try
                {
                    _currentGameState = await _smsCommandProcessors[smsMessage.ToUpper()](smsMessage);
                }
                catch (KeyNotFoundException)
                {
                    response = "invalid request";
                }
            }

            return response;
        }

        public Task<List<StoryPoint>> Initialise()
        {
            _storyTree = new List<StoryPoint>();
            _currentStoryParents = new List<StoryPoint>();
            _currentStoryPoints = new List<StoryPoint>();
            _currentGameState = new GameState();

            return PopulateStoryTree();
        }

        private Task<List<StoryPoint>> PopulateStoryTree()
        {
            return _dataAccessor.FetchAllStoryPoints();
        }

        private void RepopulateStoryTree()
        {
            _storyTree.Clear();
            _currentStoryParents.Clear();
            _currentStoryPoints.Clear();

            PopulateStoryTree();

            _currentStoryParents.Add(_storyTree.First(x => x.Parent == "nothing"));
        }

        public void GoBack()
        {
            if (_currentStoryParents.Count > 1)
            {
                _currentStoryParents.Remove(_currentStoryParents[_currentStoryParents.Count - 1]);

                LoadOptions();
            }
            else
            {
                LoadOptions();
            }
        }

        public async Task<GameState> GetPreviousOptions(string smsCommand)
        {
            GameState result = null;

            GoBack();

            result = UpdateAndReturnCurrentGameState();

            return result;
        }

        public void GoForward(int optionNumber)
        {
            if (optionNumber <= _currentStoryPoints.Count)
            {
                _currentStoryParents.Add(_currentStoryPoints[optionNumber - 1]);

                LoadOptions();
            }
        }

        public GameState GetNextOptions(int optionNumber)
        {
            GameState result = null;

            if (optionNumber <= _currentStoryPoints.Count)
            {
                GoForward(optionNumber);
                result = UpdateAndReturnCurrentGameState();
            }
            else
            {
                result = new GameState
                {
                    GameOptions = new List<GameOption>
                    {
                        new GameOption
                        {
                            OptionNumber = 0,
                            Option = "Can't find specified option number."
                        }
                    }
                }; 
            }

            return result;
        }

        private GameState UpdateAndReturnCurrentGameState()
        {
            var currentGameState = new GameState
                {
                    GameOptions = new List<GameOption>()
                };

            LoadOptions();

            for (int optionCount = 1; optionCount <= _currentStoryPoints.Count; optionCount++)
            {
                currentGameState.GameOptions.Add(new GameOption
                {
                    OptionNumber = optionCount,
                    Option = _currentStoryPoints[optionCount - 1].Child
                });
            }

            return currentGameState;
        }

        private void LoadOptions()
        {
            _currentStoryPoints = _storyTree.Where(x => x.Parent == _currentStoryParents[_currentStoryParents.Count - 1].Child).ToList();
        }
    }
}
