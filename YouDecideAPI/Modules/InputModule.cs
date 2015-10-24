using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace YouDecideAPI.Modules
{
    public class InputModule : NancyModule
    {
        public InputModule()
        {
            Get["/input"] = parameters =>
            {
                string optionChosen = Request.Query["option"];

                return string.Format("option: {0}", optionChosen);
            };
        }
    }
}