using ALDEED_HFT_2021221.Data;
using ALDEED_HFT_2021221.Logic;
using ALDEED_HFT_2021221.Models;
using ALDEED_HFT_2021221.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Test
{
    [TestFixture]
    public class LogicTesting
    {
        IPlanetsRepository planetRepository;
        ILocationRepository locationRepository;
        IPlayerRepository playerRepository;
        IPvpEventRepository pvpEventRepository;

        PlanetLogic planetLogic;
        LocationLogic locationLogic;
        PlayerLogic playerLogic;
        PvpEventLogic pvpEventLogic;

        //non-crud tests
        [Test]
        public void WherePlayerWasKilled()
        {
            LocationLogic locationLogic = new LocationLogic(locationRepository, playerRepository, pvpEventRepository);
            Assert.That(() => locationLogic.WherePlayerWasKilled(2).LocationName, Is.EqualTo("Mountain"));
            Assert.That(() => locationLogic.WherePlayerWasKilled(4).LocationName, Throws.Exception);
        }

        [Test]
        public void WhichPvpEventWonByThisPlayer()
        {
            PvpEventLogic pvpEventLogic = new PvpEventLogic(pvpEventRepository, locationRepository, playerRepository);
            Assert.That(() => pvpEventLogic.whichPvpEventWonByThisPlayer(1).PvpEventName, Is.EqualTo("The Crucible"));
            Assert.That(() => pvpEventLogic.whichPvpEventWonByThisPlayer(33333), Throws.Exception);
        }

        [Test]
        public void NonCrudPlanet()
        {
            PlanetLogic planetLogic = new PlanetLogic(planetRepository, playerRepository, pvpEventRepository);
            Assert.That(() => planetLogic.NonCrudMap("The Crucible").PlanetName, Is.EqualTo("Mars"));
        }

        [Test]
        public void whichPvpEvent()
        {
            PvpEventLogic pvpEventLogic = new PvpEventLogic(pvpEventRepository, locationRepository, playerRepository);
            Assert.That(() => pvpEventLogic.whichPvpEventInThisLocation("Canyon").PvpEventName, Is.EqualTo("The Crucible"));
        }

        [TestCase(1)]
        public void WhoWonThisPvpEvent(int pvpEventId)
        {
            PlayerLogic logic = new PlayerLogic(playerRepository, pvpEventRepository);
            Assert.That(() => logic.whoWonThisPvpEvent(pvpEventId).PlayerId, Is.EqualTo(1));
        }

        //Crud tests

        [Test]
        public void GetPvpEventByIdTest()
        {
            var got = pvpEventLogic.getPvpEventById(2);
            Assert.That(got.PvpEventName == ("Arena"));
        }

        [Test]
        public void GetPlanetByIdTest()
        {
            var get = planetLogic.getPlanetById(1);
            Assert.That(get.PlanetName == ("Earth"));
        }

        [Test]
        public void ChangePlayerStatus()
        {
            playerLogic.changeKilledOrNot(1, false, 3);
            Assert.That(playerLogic.getPlayerById(2).EliminatedOnPlanet_PlanetId, Is.Not.Null);
        }

        [Test]
        public void ChangeLocation()
        {
            locationLogic.changeLocation(3, "Kistarcsa");
            Assert.That(locationLogic.getLocationById(3).LocationName == "Kistarcsa");
        }


        [Test]
        public void AddPlanetTest()
        {
            Assert.That(() => planetLogic.addPlanet(new Planets() { PlanetId = 4, PlanetName = "Titan", Difficulty = 9 }), Throws.InvalidOperationException);
        }

        [Test]
        public void AddLocationTest()
        {
            Assert.That(() => locationLogic.addLocation(new Locations() { LocationId = 5, LocationName = "Shroomland" }), Throws.InvalidOperationException);
        }

        [Test]
        public void AddPvpEventTest()
        {
            Assert.That(() => pvpEventLogic.newPvpEvent(new PvpEvent() { PvpEventId = 5, PvpEventName = "Capture the Flag", LocationId = 2 }), Throws.InvalidOperationException);
        }

        [SetUp]
        public void Setup()
        {

            List<Planets> planetData =
                new List<Planets>
                {
                    new Planets() { PlanetId = 1, PlanetName = "Earth", Difficulty = 1 },
                    new Planets() { PlanetId = 2, PlanetName = "Mars", Difficulty = 3 },
                    new Planets() { PlanetId = 3, PlanetName = "Venus", Difficulty = 2 }
                };
            List<Locations> locationData =
                new List<Locations>
                {
                    new Locations() { LocationId = 1, LocationName = "Canyon"},
                    new Locations() { LocationId = 2, LocationName = "Mountain" },
                    new Locations() { LocationId = 3, LocationName = "City" },
                    new Locations() { LocationId = 4, LocationName = "Tower" }
                };
            List<Players> playerData =
                new List<Players>
                {
                    new Players() { PlayerId = 1, PlayerName = "Parzival", PlayerLevel = 10, IsEliminated = false, PvpEventId = 1 },
                    new Players() { PlayerId = 2, PlayerName = "H", PlayerLevel = 13, IsEliminated = true, EliminatedOnPlanet_PlanetId = 2, PvpEventId = 2 },
                    new Players() { PlayerId = 3, PlayerName = "Artemis", PlayerLevel = 20, IsEliminated = false, PvpEventId = 1 },
                    new Players() { PlayerId = 4, PlayerName = "ASDSAS", PlayerLevel = 9, IsEliminated = false, PvpEventId = 2 },
                    new Players() { PlayerId = 5, PlayerName = "iceman", PlayerLevel = 11, IsEliminated = true, EliminatedOnPlanet_PlanetId = 3, PvpEventId = 6 },
                    new Players() { PlayerId = 6, PlayerName = "weedman", PlayerLevel = 12, IsEliminated = true, EliminatedOnPlanet_PlanetId = 2, PvpEventId = 3 },
                    new Players() { PlayerId = 7, PlayerName = "weedwoman", PlayerLevel = 13, IsEliminated = false, PvpEventId = 4 },
                    new Players() { PlayerId = 8, PlayerName = "fuckiewuckie", PlayerLevel = 6, IsEliminated = true, EliminatedOnPlanet_PlanetId = 1, PvpEventId = 5 },
                    new Players() { PlayerId = 9, PlayerName = "myman", PlayerLevel = 4, IsEliminated = false, PvpEventId = 5 }
                 };
            List<PvpEvent> pvpEventData =
                new List<PvpEvent>
                {
                    new PvpEvent() { PvpEventId = 1, PvpEventName = "The Crucible", LocationId = 1 },
                    new PvpEvent() { PvpEventId = 2, PvpEventName = "Arena", LocationId = 2 },
                    new PvpEvent() { PvpEventId = 3, PvpEventName = "King of the Hill", LocationId = 2 },
                    new PvpEvent() { PvpEventId = 4, PvpEventName = "Payload", LocationId = 4 },
                    new PvpEvent() { PvpEventId = 5, PvpEventName = "Deathmatch", LocationId = 3 },
                    new PvpEvent() { PvpEventId = 6, PvpEventName = "Battle Royale", LocationId = 1 },
                };

            Mock<VideoGameDbContext> contextMock = new Mock<VideoGameDbContext>();

            Mock<DbSet<Planets>> planetDbSetMock = new Mock<DbSet<Planets>>();
            Mock<DbSet<Locations>> locationDbSetMock = new Mock<DbSet<Locations>>();
            Mock<DbSet<Players>> playerDbSetMock = new Mock<DbSet<Players>>();
            Mock<DbSet<PvpEvent>> pvpEventDbSetMock = new Mock<DbSet<PvpEvent>>();

            planetRepository = new PlanetRepository(contextMock.Object);
            locationRepository = new LocationRepository(contextMock.Object);
            playerRepository = new PlayerRepository(contextMock.Object);
            pvpEventRepository = new PvpEventRepository(contextMock.Object);

            IQueryable<Planets> queryPlanets = planetData.AsQueryable();
            IQueryable<Locations> queryLocations = locationData.AsQueryable();
            IQueryable<Players> queryPlayers = playerData.AsQueryable();
            IQueryable<PvpEvent> queryPvpEvent = pvpEventData.AsQueryable();

            planetDbSetMock.As<IQueryable<Planets>>()
                .Setup(mock => mock.Provider)
                .Returns(queryPlanets.Provider);

            planetDbSetMock.As<IQueryable<Planets>>()
                .Setup(mock => mock.Expression)
                .Returns(queryPlanets.Expression);

            planetDbSetMock.As<IQueryable<Planets>>()
                .Setup(mock => mock.ElementType)
                .Returns(queryPlanets.ElementType);

            planetDbSetMock.As<IQueryable<Planets>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queryPlanets.GetEnumerator());


            locationDbSetMock.As<IQueryable<Locations>>()
                .Setup(mock => mock.Provider)
                .Returns(queryLocations.Provider);

            locationDbSetMock.As<IQueryable<Locations>>()
                .Setup(mock => mock.Expression)
                .Returns(queryLocations.Expression);

            locationDbSetMock.As<IQueryable<Locations>>()
                .Setup(mock => mock.ElementType)
                .Returns(queryLocations.ElementType);

            locationDbSetMock.As<IQueryable<Locations>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queryLocations.GetEnumerator());


            playerDbSetMock.As<IQueryable<Players>>()
                .Setup(mock => mock.Provider)
                .Returns(queryPlayers.Provider);

            playerDbSetMock.As<IQueryable<Players>>()
                .Setup(mock => mock.Expression)
                .Returns(queryPlayers.Expression);

            playerDbSetMock.As<IQueryable<Players>>()
                .Setup(mock => mock.ElementType)
                .Returns(queryPlayers.ElementType);

            playerDbSetMock.As<IQueryable<Players>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queryPlayers.GetEnumerator());


            pvpEventDbSetMock.As<IQueryable<PvpEvent>>()
                .Setup(mock => mock.Provider)
                .Returns(queryPvpEvent.Provider);

            pvpEventDbSetMock.As<IQueryable<PvpEvent>>()
                .Setup(mock => mock.Expression)
                .Returns(queryPvpEvent.Expression);

            pvpEventDbSetMock.As<IQueryable<PvpEvent>>()
                .Setup(mock => mock.ElementType)
                .Returns(queryPvpEvent.ElementType);

            pvpEventDbSetMock.As<IQueryable<PvpEvent>>()
                .Setup(mock => mock.GetEnumerator())
                .Returns(queryPvpEvent.GetEnumerator());


            contextMock.Setup(mock => mock.Set<Planets>()).Returns(planetDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<Locations>()).Returns(locationDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<Players>()).Returns(playerDbSetMock.Object);
            contextMock.Setup(mock => mock.Set<PvpEvent>()).Returns(pvpEventDbSetMock.Object);

            planetLogic = new PlanetLogic(planetRepository, playerRepository, pvpEventRepository);
            pvpEventLogic = new PvpEventLogic(pvpEventRepository, locationRepository, playerRepository);
            playerLogic = new PlayerLogic(playerRepository, pvpEventRepository);
            locationLogic = new LocationLogic(locationRepository, playerRepository, pvpEventRepository);
        }
    }
}
