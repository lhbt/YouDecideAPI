using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public sealed class StoryNavigator : IStoryNavigator
    {
        public StoryNavigator(IDataAccess dataAccessor)
        {
        }

        public string ProcessSMSInput(string smsMessage)
        {
            return "not implemented yet.";
        }
    }
}
