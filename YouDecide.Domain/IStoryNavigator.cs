namespace YouDecide.Domain
{
    public interface IStoryNavigator
    {
        string ProcessSMSInput(string smsMessage);
    }
}