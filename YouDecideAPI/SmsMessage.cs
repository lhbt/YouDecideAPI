namespace YouDecideAPI
{
    internal class SmsMessage
    {
        public string Id="Unset";

        public string To = "Unset";

        public string From = "Unset";

        public string Keyword = "Unset";

        public string Content = "Unset";

        public string FullMessage()
        {
            return Id + " " + To + " " + From + " " + Keyword + " " + Content;
        }
    }
}