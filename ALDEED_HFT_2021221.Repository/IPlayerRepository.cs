using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public interface IPlayerRepository : IRepository<Players>
    {
        void addPlayer(Players player);
        void removePlayer(int id);
        void changeKilledOrNot(int id, bool isEliminated, int killedOnPlanetId);
    }
}
