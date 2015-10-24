using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace YouDecideAPI.Modules
{
    public class StartModule : NancyModule
    {
        public StartModule()
        {
            Get["/start"] = parameters =>
            {
                return "initialised";
            };
        }
    }
}