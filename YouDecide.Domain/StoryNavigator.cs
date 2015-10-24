using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public sealed class StoryNavigator : IStoryNavigator
    {
        private readonly Dictionary<string, ProcessSMSCommand> _smsCommandProcessors;
        private delegate GameState ProcessSMSCommand(string smsCommand);

        static List<StoryPoint> _storyTree;
        static List<StoryPoint> _currentStoryParents;
        static List<StoryPoint> _currentStoryPoints;

        public StoryNavigator(IDataAccess dataAccessor)
        {
            _smsCommandProcessors
                = new Dictionary<string, ProcessSMSCommand>
                    {
                        {"START", StartGame},
                        {"BACK", GetPreviousOptions}
                    };
        }

        private GameState StartGame(string smsCommand)
        {
            return new GameState();
        }

        public string ProcessSMSInput(string smsMessage)
        {
            string response = "Thank you for your input: " + smsMessage;

            if (smsMessage.All(Char.IsDigit))
            {
                GetNextOptions(int.Parse(smsMessage));
            }
            else
            {
                try
                {
                    _smsCommandProcessors[smsMessage.ToUpper()](smsMessage);
                }
                catch (KeyNotFoundException)
                {
                    response = "invalid request";
                }
            }

            return response;
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

        public GameState GetPreviousOptions(string smsCommand)
        {
            GameState result = null;

            GoBack();

            result = GetCurrentGameState();

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
                result = GetCurrentGameState();
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

        private GameState GetCurrentGameState(bool firstTimeIn = false)
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
