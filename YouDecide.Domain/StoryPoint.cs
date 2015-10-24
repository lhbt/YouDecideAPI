using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public class StoryPoint
    {
        public int Id { get; set; }
        public string Parent { get; set; }
        public string Child { get; set; }
    }
}
