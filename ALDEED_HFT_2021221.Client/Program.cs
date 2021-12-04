using ConsoleTools;
using ALDEED_HFT_2021221.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ALDEED_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {            
            Rest rest = new Rest("http://localhost:15675");

            rest.Post<Planets>(new Planets()
            {
                PlanetName = "Mars",
                Difficulty = 43
            }, "planets");

            var planets = rest.Get<Planets>("planets");

            foreach (var planet in planets)
            {
                Console.WriteLine(planet.PlanetName);
            }

            var planetMenu = new ConsoleMenu()
                .Add("Add new planet", () => AddPlanet(rest))
                .Add("Return all planets", () => GetAllPlanets(rest))
                .Add("Show planet by id", () => GetPlanet(rest))
                .Add("Rename a planet", () => RenamePlanet(rest))
                .Add("Delete a planet", () => DeletePlanet(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var locationMenu = new ConsoleMenu()
                .Add("Add new lovcation", () => CreateLocation(rest))
                .Add("Return all locations", () => GetAllLocations(rest))
                .Add("Show location by id", () => GetLocation(rest))
                .Add("Change location", () => ChangeLocation(rest))
                .Add("Delete a location", () => DeleteLocation(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var playerMenu = new ConsoleMenu()
                .Add("Add new player", () => CreatePlayer(rest))
                .Add("Return all players", () => GetAllPlayers(rest))
                .Add("Show player by id", () => GetPlayer(rest))
                .Add("Kill a player", () => KillPlayer(rest))
                .Add("Delete a player", () => DeletePlayer(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var pvpEventMenu = new ConsoleMenu()
                .Add("Create new PvP event", () => CreatePvpEvent(rest))
                .Add("Show all events", () => GetAllPvpEvents(rest))
                .Add("Show event by id", () => GetPvpEvent(rest))
                .Add("Rename an event", () => RenamePvpEvent(rest))
                .Add("Delete an event", () => DeletePvpEvent(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var noncrudMenu = new ConsoleMenu()
                .Add("Where was this player killed?", () => WhereThisPlayerWasKilled(rest))
                .Add("Where did most players die?", () => WhereDidMostPlayersDie(rest))
                .Add("Who won a certain event?", () => WhoWonThisPvpEvent(rest))
                .Add("Which event was won by a certain player?", () => WhichEventWonByThisPlayer(rest))
                .Add("Main menu", ConsoleMenu.Close);

            var mainMenu = new ConsoleMenu()
                .Add("Planet editor", () => planetMenu.Show())
                .Add("Location editor", () => locationMenu.Show())
                .Add("Player editor", () => playerMenu.Show())
                .Add("PvP Event editor", () => pvpEventMenu.Show())
                .Add("Run non-crud stuff", () => noncrudMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            mainMenu.Show();

            Console.ReadKey();
        }

        //planet crud
        private static void AddPlanet(Rest restService)
        {
            Console.WriteLine("new planet's name: ");
            string name = Console.ReadLine();
            Console.WriteLine("difficulty (1-9): ");
            int difficulty = int.Parse(Console.ReadLine());
            restService.Post(new Planets
            {
                PlanetName = name,
                Difficulty = difficulty
            }, "planets");
            Console.WriteLine("a new world has been generated");
            Console.ReadKey();
        }

        private static void GetAllPlanets(Rest restService)
        {
            var planets = restService.Get<Planets>("planets");
            foreach (var planet in planets)
            {
                Console.WriteLine(planet.PlanetName);
            }
            Console.ReadKey();
        }

        private static void GetPlanet(Rest restService)
        {
            try
            {
                Console.WriteLine("planet id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Planets planet = restService.Get<Planets>(id, "planets");
                Console.WriteLine($"ihe planet with this id is {planet.PlanetName},difficulty {planet.Difficulty}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void RenamePlanet(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the planet to be renamed: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Planets planet = restService.Get<Planets>(id, "planets");
                string previousName = planet.PlanetName;
                Console.WriteLine("new name of the planet: ");
                string newName = Console.ReadLine();
                planet.PlanetName = newName;
                restService.Put(planet, "planets");
                Console.WriteLine($"{previousName} has been renamed to {newName}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeletePlanet(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the planet to be deleted: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Planets>(id, "planets").Players.Count() == 0)
                {
                    restService.Delete(id, "planets");
                    Console.WriteLine($"Planet ({id}) is no more.");
                }
                else
                {
                    Console.WriteLine("remove the dead");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        //location crud
        private static void CreateLocation(Rest restService)
        {
            Console.WriteLine("name of new location: ");
            string name = Console.ReadLine();
            restService.Post(new Locations
            {
                LocationName = name,
            }, "locations");
            Console.WriteLine("new location available");
            Console.ReadKey();
        }

        private static void GetAllLocations(Rest restService)
        {
            var locations = restService.Get<Locations>("locations");
            foreach (var location in locations)
            {
                Console.WriteLine($"{location.LocationName}");
            }
            Console.ReadKey();
        }

        private static void GetLocation(Rest restService)
        {
            try
            {
                Console.WriteLine("location id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Locations location = restService.Get<Locations>(id, "locations");
                Console.WriteLine($"The location with this id is {location.LocationName}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void ChangeLocation(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the location to be changed: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Locations location = restService.Get<Locations>(id, "locations");
                string previousLocation = location.LocationName;
                Console.WriteLine("new location: ");
                string newLocation = Console.ReadLine();
                location.LocationName = newLocation;
                restService.Put(location, "locations");
                Console.WriteLine($"{previousLocation} is now {newLocation}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeleteLocation(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the location to be deleted: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Locations>(id, "locations").PvpEvents.Count() == 0)
                {
                    restService.Delete(id, "locations");
                    Console.WriteLine($"Location ({id}) has been deleted.");
                }
                else
                {
                    Console.WriteLine("remove pvp events from the area");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void CreatePlayer(Rest restService)
        {
            Console.WriteLine("name the new player: ");
            string name = Console.ReadLine();
            Console.WriteLine("player level");
            int level = int.Parse(Console.ReadLine());
            Console.WriteLine("give the id of a pvp event to register: ");
            int pvpEventId = Convert.ToInt32(Console.ReadLine());
            restService.Post(new Players
            {
                PlayerName = name,
                PvpEventId = pvpEventId
            }, "players"); ;
            Console.WriteLine($"the new player registered at event {pvpEventId}");
            Console.ReadKey();
        }

        private static void GetAllPlayers(Rest restService)
        {
            var players = restService.Get<Players>("players");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.PlayerName} played in event {player.PvpEventId}");
            }
            Console.ReadKey();
        }

        private static void GetPlayer(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the player: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Players player = restService.Get<Players>(id, "players");
                Console.WriteLine($"Tte player with id {player.PlayerName} fought an epic battle in {player.PvpEvent}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void KillPlayer(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the killed player: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Players player = restService.Get<Players>(id, "players");
                if (player.EliminatedOnPlanet_PlanetId == null)
                {
                    Console.WriteLine("id of the planet where the player was killed: ");
                    int planetId = Convert.ToInt32(Console.ReadLine());
                    player.IsEliminated = false;
                    player.EliminatedOnPlanet_PlanetId = planetId;
                    restService.Put(player, "players");
                    Console.WriteLine($"player {player.PlayerId} is no more.");
                }
                else Console.WriteLine("there's no point in killing the dead");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeletePlayer(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the player to be deleted: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<Players>("players").Equals(null))
                {
                    Console.WriteLine("no such player");
                }
                else
                {
                    restService.Delete(id, "players");
                    Console.WriteLine($"player {id} has been let go of.");
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
        //pvp crud
        private static void CreatePvpEvent(Rest restService)
        {
            Console.WriteLine("name the new event: ");
            string name = Console.ReadLine();
            Console.WriteLine("give a location id");
            int locationId = Convert.ToInt32(Console.ReadLine());
            restService.Post(new PvpEvent
            {
                PvpEventName = name,
                LocationId = locationId
            }, "pvp events"); 
            Console.WriteLine($"event {restService.Get<PvpEvent>("pvp events").Last().PvpEventId} now playable");
            Console.ReadKey();
        }

        private static void GetAllPvpEvents(Rest restService)
        {
            var pvpEvents = restService.Get<PvpEvent>("pvp events");
            foreach (var pvpEvent in pvpEvents)
            {
                Console.WriteLine($"event {pvpEvent.PvpEventId}: {pvpEvent.PvpEventName}");
            }
            Console.ReadKey();
        }

        private static void GetPvpEvent(Rest restService)
        {
            try
            {
                Console.WriteLine("event id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                PvpEvent pvpEvent = restService.Get<PvpEvent>(id, "pvp event");
                Console.WriteLine($"event {id} is {pvpEvent.PvpEventName}.");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void RenamePvpEvent(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the event to be renamed: ");
                int id = Convert.ToInt32(Console.ReadLine());
                PvpEvent pvpEvent = restService.Get<PvpEvent>(id, "pvp event");
                string previousName = pvpEvent.PvpEventName;
                Console.WriteLine("name the new event: ");
                string newName = Console.ReadLine();
                pvpEvent.PvpEventName = newName;
                restService.Put(pvpEvent, "pvp event");
                Console.WriteLine($"{previousName} is now {newName}");
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }

        private static void DeletePvpEvent(Rest restService)
        {
            try
            {
                Console.WriteLine("id of the event to be deleted: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (restService.Get<PvpEvent>(id, "pvp event").Players.Count() == 0)
                {
                    restService.Delete(id, "pvp event");
                    Console.WriteLine($"event {id} is not playable anymore");
                }
                else
                {
                    Console.WriteLine("remove players connected to the event");
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Invalid id");
            }
            Console.ReadKey();
        }
        //non-crud
        private static void WhereThisPlayerWasKilled(Rest restService)
        {
            Console.WriteLine("id of the player in question: ");
            int playerId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var locations = restService.Get<Locations>("locations", "wherePlayerWasKilled", playerId);
                Console.WriteLine($"their body is in {locations.LocationName}");
            }
            catch (Exception)
            {
                Console.WriteLine("player is still awaiting death or does not exist");
            }
            Console.ReadKey();
        }

        private static void WhereDidMostPlayersDie(Rest restService)
        {
            Console.WriteLine("enter the name of the event to learn where the most players died");
            string pvpEventName = Console.ReadLine();
            try
            {
                var planet = restService.Get<Planets>("planets", "noncrudmap", pvpEventName);
                Console.WriteLine($"The grave of most players in {pvpEventName} was {planet.PlanetName}");
            }
            catch (Exception)
            {
                Console.WriteLine("no such event");
            }
            Console.ReadKey();
        }

        private static void WhoWonThisPvpEvent(Rest restService)
        {
            Console.WriteLine("id of the event");
            int pvpEventId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var player = restService.Get<Players>("players", "whowonthispvpevent", pvpEventId);
                Console.WriteLine($"the winner of {player.PvpEventId} was {player.PlayerName}");
            }
            catch (Exception)
            {
                Console.WriteLine("no such event");
            }
            Console.ReadKey();
        }

        private static void WhichEventWonByThisPlayer(Rest restService)
        {
            Console.WriteLine("give player id ");
            int playerId = Convert.ToInt32(Console.ReadLine());
            try
            {
                var pvpEvent = restService.Get<PvpEvent>("pvp event", "WhichEventWonByThisPlayer", playerId);
                Console.WriteLine($"Player {playerId} is the winner of {pvpEvent.PvpEventId}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Player {playerId} either does not exist, or already dead.");
            }
            Console.ReadKey();
        }
    }
}
