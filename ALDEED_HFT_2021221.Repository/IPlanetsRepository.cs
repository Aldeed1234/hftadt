using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public interface IPlanetsRepository : IRepository<Planets>
    {
        void addPlanet(Planets planet);
        void removePlanet(int id);
        void renamePlanet(int id, string newPlanetName);
    }
}
