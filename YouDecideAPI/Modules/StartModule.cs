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
                int count = 0;

                try
                {
                    List<StoryPoint> data = (await dataAccessor.FetchAllStoryPoints());
                    count = data.Count;
                }
                catch (Exception ex)
                {
                    // Ignore.
                }

                return Response.AsJson(string.Format("initial game state - {0} story points retrieved.", count));
            };
        }
    }
}