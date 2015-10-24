using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IStoryNavigator
    {
        string ProcessSMSInput(string smsMessage);
        Task<GameState> ProcessSMSInputReturningGameState(string smsMessage);
    }
}