using Nancy;
using Newtonsoft.Json;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class InputSMSModule : NancyModule
    {
        public InputSMSModule(IStoryNavigator storyNavigator)
        {
            Get["/inputsms", true] = async (parameters, ct) =>
            {
                string smsMessage = Request.Query["content"];

                GameState currentState = await storyNavigator.ProcessSMSInputReturningGameState(smsMessage);

                return JsonConvert.SerializeObject(currentState);

                //string nicelyFormattedOptions = await storyNavigator.ProcessSMSInput(smsMessage);

                //return nicelyFormattedOptions;
            };
        }
    }
}