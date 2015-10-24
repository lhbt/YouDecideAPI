using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IStoryNavigator
    {
        Task<string> ProcessSMSInput(string smsMessage);
        Task<GameState> ProcessSMSInputReturningGameState(string smsMessage);
        GameState GetCurrentGameState();
    }
}