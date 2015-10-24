using YouDecide.Domain;

using Nancy;

namespace YouDecideAPI.Modules
{
    public class StartModule : NancyModule
    {
        public StartModule(IDataAccess dataAccessor)
        {
            Get["/start", true] = async (parameters, ct) =>
            {
                var data = (await dataAccessor.FetchAllStoryPoints());
                var count = data.Count;

                return Response.AsJson(string.Format("initial game state - {0} story points retrieved.", count));
            };
        }
    }
}