﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDecide.Domain
{
    public interface IDataAccess
    {
        Task<List<StoryPoint>> FetchAllStoryPoints();
    }
}
