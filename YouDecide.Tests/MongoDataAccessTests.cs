//using System.Linq;
//using YouDecide.Mongo;

//using NUnit.Framework;

//namespace YouDecide.Tests
//{
//    using System.Collections.Generic;

//    using YouDecide.Domain;

//    [TestFixture]
//    public class MongoDataAccessTests
//    {
//        [Test]
//        public async void ShouldFetchAllStoryPoints()
//        {
//            var mongoDataAccess = new MongoDataAccess();

//            var data = await mongoDataAccess.FetchAllStoryPoints();

//            Assert.AreEqual(229, data.Count);
//        }

//        [Test]
//        public async void should_do_something()
//        {
//            var sut = new MongoDataAccess();
//            var gameId = "1234";
//            await sut.UpdateGameState(new GameState { GameId = gameId });
//            var result = sut.GetCurrentGameState(gameId);
//            Assert.That(result.GameId, Is.EqualTo(gameId));
//        }

//        [Test]
//        public async void should_insert_then_retrieve_parents()
//        {
//            var sut = new MongoDataAccess();
//            var storyPoint = new StoryPoint() { Child = "test child", Id = 1234, Parent = "parent test" };
//            var storyPoints = new List<StoryPoint>() { storyPoint };
//            await sut.UpdateGameState(new GameState { GameId = "gameIdTest" });
//            await sut.UpdateGameParents(storyPoints, "gameIdTest");

//            var result = sut.GetGameStoryParents("gameIdTest");

//            Assert.That(result.First().Parent, Is.EqualTo(storyPoint.Parent));
//            Assert.That(result.First().Child, Is.EqualTo(storyPoint.Child));
//            Assert.That(result.First().Id, Is.EqualTo(storyPoint.Id));
//        }
//    }
//}
