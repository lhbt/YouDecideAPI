using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace YouDecide.Mongo
{
    public static class StoryPointExtensions
    {
        public static BsonDocument ConvertToBsonDocument(this Domain.StoryPoint source)
        {
            return new BsonDocument
            {
                {"_id", source.Id},
                {"parent", source.Parent},
                {"child", source.Child}
            };
        }
    }
}
