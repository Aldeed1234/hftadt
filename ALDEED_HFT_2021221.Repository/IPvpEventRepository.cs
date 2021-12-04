using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public interface IPvpEventRepository : IRepository<PvpEvent>
    {
        void addPvpEvent(PvpEvent pvpEvent);
        void removePvpEvent(int id);
        void renamePvpEvent(int id, string newPvpEventName);
    }
}
