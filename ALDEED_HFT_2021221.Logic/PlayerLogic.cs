using ALDEED_HFT_2021221.Models;
using ALDEED_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    //please show mercy
    public class PlayerLogic : IPlayerLogic
    {
        IPlayerRepository playerRepository;
        IPvpEventRepository pvpEventRepository;

        public PlayerLogic(IPlayerRepository repository, IPvpEventRepository pvpEventRepository1)
        {
            this.playerRepository = repository;
            pvpEventRepository = pvpEventRepository1;
        }

        public void changeKilledOrNot(int id, bool newStatus, int killedOnPlanetId)
        {
            playerRepository.changeKilledOrNot(id, newStatus, killedOnPlanetId);
        }

        public IList<Players> getAllPlayers()
        {
            return playerRepository.GetAll().ToList();
        }

        public Players getPlayerById(int id)
        {
            return playerRepository.GetOne(id);
        }

        public void addPlayer(Players player)
        {
            playerRepository.addPlayer(player);
        }

        public void removePlayer(int id)
        {
            playerRepository.removePlayer(id);
        }


        //Non-Crud
        public Players whoWonThisPvpEvent(int pvpEventId)
        {
            IQueryable<PvpEvent> PvpEvent = pvpEventRepository.GetAll();
            ICollection<Players> players = playerRepository.GetAll().Where(x => x.PvpEventId == pvpEventId).ToList();
            PvpEvent pvpEvents = PvpEvent.Where(x => x.PvpEventId == pvpEventId).FirstOrDefault();
            pvpEvents.Players = players;

            if (pvpEvents.Players.Count(x => x.IsEliminated == true) > 1)
                throw new PvpEventNotSurvivedException();
            else
            {
                Players toReturn = pvpEvents.Players.Where(x => x.IsEliminated == true).First();
                return toReturn;
            }
        }
    }
}
