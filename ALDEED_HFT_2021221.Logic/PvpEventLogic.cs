using ALDEED_HFT_2021221.Models;
using ALDEED_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public class PvpEventLogic : IPvpEventLogic
    {
        IPvpEventRepository pvpEventRepository;
        ILocationRepository locationRepository;
        IPlayerRepository playerRepository;

        public PvpEventLogic(IPvpEventRepository repository, ILocationRepository locationRepository1, IPlayerRepository playerRepository1)
        {
            this.pvpEventRepository = repository;
            locationRepository = locationRepository1;
            playerRepository = playerRepository1;
        }

        public void changePvpEventName(int id, string newName)
        {
            pvpEventRepository.renamePvpEvent(id, newName);
        }

        public IList<PvpEvent> getAllPvpEvents()
        {
            return pvpEventRepository.GetAll().ToList();
        }

        public PvpEvent getPvpEventById(int id)
        {
            return pvpEventRepository.GetOne(id);
        }

        public void newPvpEvent(PvpEvent pvpEvent)
        {
            pvpEventRepository.addPvpEvent(pvpEvent);
        }

        public void removePvpEvent(int id)
        {
            pvpEventRepository.removePvpEvent(id);
        }

        //Non-Crud
        //Where was the first pvp event?
        //public string whereFirstPvp(string locationName)
        //{
        //    ICollection<PvpEvent> pvpEvents = pvpEventRepository.GetAll().ToList();
        //    IQueryable<Locations> locations = locationRepository.GetAll();

        //    Locations location = locations.Where(x => x.LocationName == locationName).FirstOrDefault();
        //    location.PvpEvents = pvpEvents.Where(x => x.LocationId == location.LocationId).ToList();

        //    string toReturn = pvpEvents.Where(x => x.LocationId == location.LocationId).Select(x => x.PvpEventName).FirstOrDefault();

        //    if (location is null)
        //        throw new InvalidLocationException();
        //    else
        //        return toReturn;
        //}

        public PvpEvent whichPvpEventInThisLocation(string locationName) //Where was the first pvp event
        {
            ICollection<PvpEvent> pvpEvents = pvpEventRepository.GetAll().ToList();
            IQueryable<Locations> locations = locationRepository.GetAll();

            Locations location = locations.Where(x => x.LocationName == locationName).FirstOrDefault();
            location.PvpEvents = pvpEvents.Where(x => x.LocationId == location.LocationId).ToList();

            PvpEvent toReturn = pvpEvents.Where(x => x.LocationId == location.LocationId).FirstOrDefault();

            if (location is null)
                throw new InvalidLocationException();
            else
                return toReturn;
        }

        public PvpEvent whichPvpEventWonByThisPlayer(int playerId)
        {
            Players player = playerRepository.GetOne(playerId);
            if (player.EliminatedOnPlanet_PlanetId != null)
            {
                throw new PlayerKilledException();
            }
            else if (player is null)
            {
                throw new NoSuchPlayerException();
            }
            else
            {
                IQueryable<PvpEvent> pvpEvents = pvpEventRepository.GetAll();
                PvpEvent toReturn = pvpEvents.Where(x => x.PvpEventId == player.PvpEventId).FirstOrDefault();
                return toReturn;
            }
        }
    }
}
