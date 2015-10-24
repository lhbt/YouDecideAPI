using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using YouDecide.Domain;
using YouDecide.Mongo;
using FluentAssertions;
using MongoDB.Bson;

namespace YouDecide.Tests
{
    [TestFixture]
    public class MongoDataAccessTests
    {
        [Test]
        public async void ShouldFetchAllStoryPoints()
        {
            var mongoDataAccess = new MongoDataAccess();

            List<StoryPoint> data = await mongoDataAccess.FetchAllStoryPoints();

            Assert.AreEqual(230, data.Count);
        }
    }
}
