using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public sealed class StoryNavigator : IStoryNavigator
    {
        private Dictionary<string, ProcessSMSCommand> _smsCommandProcessors;

        private delegate GameState ProcessSMSCommand(string smsCommand);

        public StoryNavigator(IDataAccess dataAccessor)
        {
            _smsCommandProcessors
                = new Dictionary<string, ProcessSMSCommand>
                    {
                        {"START", StartGame},
                        {"BACK", GoBack}
                    };
        }

        private GameState StartGame(string smsCommand)
        {
            return new GameState();
        }

        private GameState GoBack(string smsCommand)
        {
            return new GameState();
        }

        private GameState GoForward(int optionNumber)
        {
            return new GameState();
        }

        public string ProcessSMSInput(string smsMessage)
        {
            string response = "Thank you for your input: " + smsMessage;

            if (smsMessage.All(Char.IsDigit))
            {
                GoForward(int.Parse(smsMessage));
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
    }
}
