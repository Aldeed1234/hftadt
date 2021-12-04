using ALDEED_HFT_2021221.Models;
using ALDEED_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public class LocationLogic : ILocationLogic
    {
        ILocationRepository locationRepository;
        IPlayerRepository playerRepository;
        IPvpEventRepository pvpEventRepository;

        public LocationLogic(ILocationRepository repository, IPlayerRepository playerRepository1, IPvpEventRepository pvpEventRepository1)
        {
            this.locationRepository = repository;
            playerRepository = playerRepository1;
            pvpEventRepository = pvpEventRepository1;
        }

        public void addLocation(Locations location)
        {
            locationRepository.addLocation(location);
        }

        public void changeLocation(int id, string newLocation)
        {
            locationRepository.changeLocation(id, newLocation);
        }

        public IList<Locations> getAllLocations()
        {
            return locationRepository.GetAll().ToList();
        }

        public Locations getLocationById(int id)
        {
            return locationRepository.GetOne(id);
        }

        public void removeLocation(int id)
        {
            locationRepository.removeLocation(id);
        }

        //Non-Crud
        public Locations WherePlayerWasKilled(int playerId)
        {
            try
            {
                Players player = playerRepository.GetOne(playerId);
                if (player.EliminatedOnPlanet_PlanetId == null)
                {
                    throw new PlayerNotKilledException();
                }
                else if (player is null)
                {
                    throw new NoSuchPlayerException();
                }
                else
                {
                    IQueryable<PvpEvent> pvpEvents = pvpEventRepository.GetAll();
                    IQueryable<Locations> locations = locationRepository.GetAll();
                    ICollection<Players> players = playerRepository.GetAll().ToList();

                    PvpEvent pvpEvent = pvpEvents.Where(x => x.PvpEventId == player.PvpEventId).FirstOrDefault();
                    Locations place = locations.Where(x => x.LocationId == pvpEvent.LocationId).FirstOrDefault();
                    return place;
                }
            }
            catch (PlayerNotKilledException)
            {
                return null;
            }
            catch (NoSuchPlayerException)
            {
                return null;
            }
        }
    }
}
