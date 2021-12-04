using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public interface IPlanetLogic
    {
        Planets getPlanetById(int id);
        void addPlanet(Planets planet);
        void deletePlanet(int id);
        void renamePlanet(int id, string newName);
        IList<Planets> getAllPlanets();

        Planets NonCrudMap(string pvpEventName); //for non-crud
    }
}
