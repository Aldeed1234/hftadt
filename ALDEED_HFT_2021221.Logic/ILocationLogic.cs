using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public interface ILocationLogic
    {
        Locations getLocationById(int id);
        void addLocation(Locations location);
        void removeLocation(int id);
        void changeLocation(int id, string newLocation);
        Locations WherePlayerWasKilled(int playerId); //for non-crud
        IList<Locations> getAllLocations();
    }
}
