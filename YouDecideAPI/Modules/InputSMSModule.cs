using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class InputSMSModule : NancyModule
    {
        public InputSMSModule(IStoryNavigator storyNavigator)
        {
            Get["/input"] = parameters =>
            {
                string smsMessage = Request.Query["content"];

                return storyNavigator.ProcessSMSInput(smsMessage);
            };
        }
    }
}