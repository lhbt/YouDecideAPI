namespace YouDecideAPI
{
    using System.Collections.Generic;

    using Nancy;
    using Nancy.ModelBinding;
    public class IndexModule : NancyModule
    {
        private static Dictionary<string,SmsMessage> nastyGlobal = new Dictionary<string, SmsMessage>();

        private static SmsMessage lastMessage = new SmsMessage();
        public IndexModule()
        {
            Get["/"] = parameters =>
                {
                    return View["index"];
                };

            Get["/status"] = parameters =>
                {
                    return "ok";
                };

            Get["/storedtexts/{id}"] = parameters =>
                {
                    var id = parameters.id;
                    string response;

                    if (nastyGlobal.ContainsKey(id))
                    {
                        response = nastyGlobal[id].FullMessage();
                    }
                    else
                    {
                        response = "No message stored for id of " + id;
                    }

                    return response;
                };

            Get["/lastmessage"] = parameters =>
                {
                    return lastMessage.FullMessage();
                };

            Get["/listallstoredtextsids/"] = parameters =>
                {
                    var keys = nastyGlobal.Keys;
                    var response = "";
                    foreach (var key in keys)
                    {
                        response += key.ToString()+"\n";
                    }
                    return response;
                };


            //Get["/inputsms/"] = parameters =>
            //    {
            //        var smsMessage = this.Bind<SmsMessage>();
            //        lastMessage = smsMessage;
            //        var response = "Key already exists";
            //        if (!nastyGlobal.ContainsKey(smsMessage.Id))
            //        {
            //            nastyGlobal.Add(smsMessage.Id, smsMessage);
            //            response = smsMessage.FullMessage();
            //        }
            //        return response;
            //    };
            
        }
    }
}