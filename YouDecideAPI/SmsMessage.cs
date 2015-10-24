namespace YouDecideAPI
{
    internal class SmsMessage
    {
        public string Id;

        public string To;

        public string From;

        public string Keyword;

        public string Content;

        public string FullMessage()
        {
            return Id + " " + To + " " + From + " " + Keyword + " " + Content;
        }
    }
}