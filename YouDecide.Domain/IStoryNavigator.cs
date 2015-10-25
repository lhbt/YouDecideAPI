namespace YouDecide.Domain
{
    public interface IStoryNavigator
    {
        string ProcessSMSInput(string smsMessage, string gameId);
        GameState ProcessSMSInputReturningGameState(string smsMessage, string gameId);
        GameState GetCurrentGameState(string gameId);
    }
}