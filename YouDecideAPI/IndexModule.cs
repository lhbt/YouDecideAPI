namespace YouDecideAPI
{
    using Nancy;
    using Nancy.ModelBinding;
    public class IndexModule : NancyModule
    {
        private static SmsMessage nastyGlobal;
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };
            Get["/status"] = parameters =>
            {
                return nastyGlobal.FullMessage();
            };
            Get["/inputtext/"] = parameters =>
            {
                nastyGlobal = this.Bind<SmsMessage>();
                return "ok: " + nastyGlobal.FullMessage();
            };
        }
    }
}