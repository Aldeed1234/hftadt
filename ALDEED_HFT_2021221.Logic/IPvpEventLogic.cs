using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public interface IPvpEventLogic
    {
        PvpEvent getPvpEventById(int id);
        void newPvpEvent(PvpEvent pvpEvent);
        void removePvpEvent(int id);
        void changePvpEventName(int id, string newName);
        IList<PvpEvent> getAllPvpEvents();
        PvpEvent whichPvpEventInThisLocation(string locationName);
        PvpEvent whichPvpEventWonByThisPlayer(int playerId);
    }
}
