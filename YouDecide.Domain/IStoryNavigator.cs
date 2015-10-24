using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IStoryNavigator
    {
        Task<string> ProcessSMSInput(string smsMessage, string gameId);
        Task<GameState> ProcessSMSInputReturningGameState(string smsMessage, string gameId);
        GameState GetCurrentGameState();
    }
}