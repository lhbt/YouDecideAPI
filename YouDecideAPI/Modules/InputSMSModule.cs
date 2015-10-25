using Nancy;
using Newtonsoft.Json;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class InputSMSModule : NancyModule
    {
        public InputSMSModule(IStoryNavigator storyNavigator)
        {
            Get["/inputsms"] = (parameters) =>
            {
                string smsMessage = Request.Query["content"];

                var currentState = storyNavigator.ProcessSMSInputReturningGameState(smsMessage, "0");

                return JsonConvert.SerializeObject(currentState);

                //string nicelyFormattedOptions = await storyNavigator.ProcessSMSInput(smsMessage, "0");

                //return nicelyFormattedOptions;
            };
        }
    }
}