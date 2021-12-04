using ALDEED_HFT_2021221.Logic;
using ALDEED_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController
    {
        ILocationLogic locationLogic1;

        public LocationsController(ILocationLogic locationLogic)
        {
            this.locationLogic1 = locationLogic;
        }

        [HttpGet]
        public IEnumerable<Locations> GetLocation()
        {
            var locations = locationLogic1.getAllLocations();
            return locations;
        }

        [HttpGet("{id}")]
        public Locations Get(int id)
        {
            return locationLogic1.getLocationById(id);
        }

        [HttpPost]
        public void Post([FromBody] Locations locations)
        {
            locationLogic1.addLocation(locations);
        }

        [HttpPut]
        public void Put([FromBody] Locations locations)
        {
            locationLogic1.changeLocation(locations.LocationId, locations.LocationName);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            locationLogic1.removeLocation(id);
        }

        [HttpGet("inwhichcityplayerdied/{playerId}")]
        public Locations WherePlayerWasKilled(int playerId)
        {
            return locationLogic1.WherePlayerWasKilled(playerId);
        }
    }
}
