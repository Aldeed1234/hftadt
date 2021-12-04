using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public interface IPlayerLogic
    {
        Players getPlayerById(int id);
        void addPlayer(Players player);
        void removePlayer(int id);
        void changeKilledOrNot(int id, bool newStatus, int killedOnPlanetId);
        
        IList<Players> getAllPlayers();

        Players whoWonThisPvpEvent(int pvpEventId);
    }
}
