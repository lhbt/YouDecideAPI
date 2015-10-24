using YouDecide.Mongo;

using NUnit.Framework;

namespace YouDecide.Tests
{
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
            var sut = new GameStateDataAccess();
            await sut.NewGame(1234);
            var result = sut.GetUser(1234).Result;
            Assert.That(result.Count, Is.GreaterThan(0));
        }
    }
}
