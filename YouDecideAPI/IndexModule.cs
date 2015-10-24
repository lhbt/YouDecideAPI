namespace YouDecideAPI
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        private static string nastyGlobal;
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };
            Get["/status"] = parameter =>
            {
                return nastyGlobal;
            };
            Get["/inputtext/"] = parameter =>
            {
                var text = this.Request.Query;
                nastyGlobal = text.id + " " + text.to + " " + text.from + " " + text.keyword + " " + text.content;
                return "ok";
            };
        }
    }
}