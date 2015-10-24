using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace YouDecide.Mongo
{
    public static class BsonDocumentExtensions
    {
        public static Domain.StoryPoint ConvertToStoryPoint(this BsonDocument source)
        {
            return BsonSerializer.Deserialize<Domain.StoryPoint>(source);
        }
    }
}