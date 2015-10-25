using System;
using System.Collections.Generic;
using System.Linq;

namespace YouDecide.Domain
{
    public class StoryNavigator : IStoryNavigator
    {
        private readonly IDataAccess _dataAccessor;
        private readonly Dictionary<string, ProcessSMSCommand> _smsCommandProcessors;
        private delegate void ProcessSMSCommand(string smsCommand);

        static List<StoryPoint> _storyTree;

        private List<StoryPoint> _currentStoryPoints;
        private List<StoryPoint> _currentStoryParents;
        private GameState _currentGameState;
        private string _deathlyDeathText;
        private string _historySuffix;
        private string _gameID;

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

        public GameState GetCurrentGameState(string gameId)
        {
            return _dataAccessor.GetCurrentGameState(gameId);
        }

        private void StartGame(string smsCommand)
        {
            _dataAccessor.DeleteGameState(_gameID);

            InitialiseGame("0");

            _currentStoryParents.Add(_storyTree.First(x => x.Parent == "nothing"));

            LoadOptions();

            UpdateAndReturnCurrentGameState();

            _dataAccessor.CreateGameState(_currentGameState);
        }

        public GameState ProcessSMSInputReturningGameState(string smsMessage, string gameId)
        {
            _gameID = gameId;
            _currentGameState = LoadGameState(gameId);
            InitialiseTurn();

            if (smsMessage.All(Char.IsDigit))
            {
                GetNextOptions(int.Parse(smsMessage));
            }
            else
            {
                _smsCommandProcessors[smsMessage.ToUpper()](smsMessage);
            }

            StoreGameState(_currentGameState, gameId);

            return _currentGameState;
        }

        private GameState LoadGameState(string gameId)
        {
            return _dataAccessor.GetCurrentGameState(gameId);
        }

        private void StoreGameState(GameState gameState, string gameId)
        {
            gameState.GameId = gameId;
            _dataAccessor.UpdateGameState(gameState);
        }

        private void LoadStoryParentsForMultiPlayer(string gameId)
        {
            _currentStoryParents = _dataAccessor.GetGameStoryParents(gameId);
        }

        private void LoadStoryPointsForMultiPlayer(string gameId)
        {
            _currentStoryPoints = _dataAccessor.GetGameStoryPoints(gameId);
        }

        public string ProcessSMSInput(string smsMessage, string gameId)
        {
            string response;

            if (smsMessage.All(Char.IsDigit))
            {
                GetNextOptions(int.Parse(smsMessage));
                response = GetOptionsNicelyFormatted();
            }
            else
            {
                try
                {
                    _smsCommandProcessors[smsMessage.ToUpper()](smsMessage);
                    response = GetOptionsNicelyFormatted();
                }
                catch (KeyNotFoundException)
                {
                    response = "invalid request";
                }
            }

            return response;
        }

        public void InitialiseGame(string gameId)
        {
            _storyTree = new List<StoryPoint>();

            _currentStoryParents = new List<StoryPoint>();
            _currentStoryPoints = new List<StoryPoint>();

            _currentGameState = new GameState
                {
                    GameId = gameId,
                    DeathlyDeathText = "",
                    History = "",
                    Parents = new List<StoryPoint>(),
                    Points = new List<StoryPoint>()
                };

            _storyTree = PopulateStoryTree();
        }

        public void InitialiseTurn()
        {
            _deathlyDeathText = "";
            _historySuffix = "";
        }

        private List<StoryPoint> PopulateStoryTree()
        {
            return _dataAccessor.FetchAllStoryPoints();
        }

        public void GoBack()
        {
            if (_currentStoryParents.Count > 1)
            {
                _currentStoryParents.Remove(MostRecentParent());
            }
            
            LoadOptions();
        }

        public void GetPreviousOptions(string smsCommand)
        {
            GoBack();

            UpdateAndReturnCurrentGameState();
        }

        public void GoForward(int optionNumber)
        {
            _currentStoryParents.Add(_currentStoryPoints[optionNumber - 1]);

            LoadOptions();
        }

        public void GetNextOptions(int optionNumber)
        {
            if (optionNumber > 0 && optionNumber <= _currentStoryPoints.Count)
            {
                GoForward(optionNumber);
                AdjustStoryPointsIfDead();
                UpdateAndReturnCurrentGameState();
            }
            else
            {
                _currentGameState = new GameState
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
        }

        private void AdjustStoryPointsIfDead()
        {
            if (WeAreDead())
            {
                _deathlyDeathText = _currentStoryPoints[0].Child;
                _historySuffix = _currentStoryPoints[0].Parent;
                GoBack();
            }
        }

        private bool WeAreDead()
        {
            return (_currentStoryPoints.Count == 1) && (!_currentStoryPoints[0].Child.Contains("WIN"));
        }

        private void UpdateAndReturnCurrentGameState()
        {
            _currentGameState.GameId = _gameID;
            _currentGameState.History = GetHistory() + " " + _historySuffix;
            _currentGameState.DeathlyDeathText = _deathlyDeathText;
            _currentGameState.GameOptions = new List<GameOption>();
            _currentGameState.Parents = _currentStoryParents;
            _currentGameState.Points = _currentStoryPoints;

            if (_currentStoryPoints.Count > 0)
            {
                for (var optionCount = 1; optionCount <= _currentStoryPoints.Count; optionCount++)
                {
                    _currentGameState.GameOptions.Add(new GameOption
                    {
                        OptionNumber = optionCount,
                        Option = _currentStoryPoints[optionCount - 1].Child
                    });
                }
            }
            else
            {
                // Now that we have the AdjustStoryPointsIfDead method, this code should never be hit. 
                // But leaving it here just in case.
                _currentGameState.GameOptions.Add(new GameOption
                {
                    OptionNumber = 0,
                    Option = "There is no point selecting this option. YOU ARE DEAD (you fool). But you can come back to life if you go back (cos we're nice like that)."
                });
            }
        }

        private string GetHistory()
        {
            var history =
                _currentStoryParents.Select(x => x.Parent == "nothing" ? "" : x.Child)
                                    .Aggregate((current, next) => string.Format("{0} {1}", current, next));

            return history;
        }

        private void LoadOptions()
        {
            _currentStoryPoints = FindTheChildrenOfTheMostRecentParent();
        }

        private List<StoryPoint> FindTheChildrenOfTheMostRecentParent()
        {
            return _storyTree.Where(x => x.Parent == MostRecentParent().Child).ToList();
        }

        private StoryPoint MostRecentParent()
        {
            return _currentStoryParents[_currentStoryParents.Count - 1];
        }

        private string GetOptionsNicelyFormatted()
        {
            string options = "No options left.";

            if (_currentStoryPoints.Count > 0)
            {
                options = _currentStoryPoints.Count == 1 ? "<br/>" : "Your options are...<br/><br/><br/><br/>";
                foreach (var storyPoint in _currentStoryPoints)
                {
                    const string either = "Either... ";
                    const string or = "Or... ";

                    var currentIndex = _currentStoryPoints.IndexOf(storyPoint);
                    var prefix = "";
                    if (_currentStoryPoints.Count > 1)
                    {
                        prefix = string.Format("{0}{1}. ", currentIndex == 0 ? either : or, currentIndex + 1);
                    }

                    options = options + string.Format("{0}{1}<br/><br/>",
                                                      prefix,
                                                      storyPoint.Child);
                }
            }

            return options;
        }
    }
}
