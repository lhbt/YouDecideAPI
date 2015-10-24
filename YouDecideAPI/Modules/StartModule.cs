using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using YouDecide.Domain;

namespace YouDecideAPI.Modules
{
    public class StartModule : NancyModule
    {
        public StartModule(IDataAccess dataAccessor)
        {
            Get["/start", true] = async (parameters, ct) =>
            {
                List<StoryPoint> data = (await dataAccessor.FetchAllStoryPoints());
                int count = data.Count;

                return Response.AsJson(string.Format("initial game state - {0} story points retrieved.", count));
            };
        }
    }
}