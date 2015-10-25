using YouDecide.Mongo;

using NUnit.Framework;

namespace YouDecide.Tests
{
    using System.Collections.Generic;

    using YouDecide.Domain;

    [TestFixture]
    public class MongoDataAccessTests
    {
        [Test]
        public async void ShouldFetchAllStoryPoints()
        {
            var mongoDataAccess = new MongoDataAccess();

            var data = await mongoDataAccess.FetchAllStoryPoints();

            Assert.AreEqual(229, data.Count);
        }

        [Test]
        public async void should_do_something()
        {
            var sut = new MongoDataAccess();
            await sut.UpdateGameState(1234);
            var result = sut.GetGameState(1234).Result;
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        public async void should_insert_then_retrieve_parents()
        {
            var sut = new MongoDataAccess();
            var storyPoint = new StoryPoint() { Child = "test child", Id = 1234, Parent = "parent test" };
            var storyPoints = new List<StoryPoint>() { storyPoint };
            await sut.UpdateGameParents(storyPoints,
                                            "gameIdTest" );

            var result = await sut.GetGameStoryParents("gameIdTest");

            Assert.That(result, Is.EqualTo(storyPoint));
        }
    }
}
