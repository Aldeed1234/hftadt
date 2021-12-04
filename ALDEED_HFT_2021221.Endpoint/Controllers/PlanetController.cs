using ALDEED_HFT_2021221.Logic;
using ALDEED_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Endpoint.Controllers
{
    public class PlanetController : Controller
    {
        IPlanetLogic planetLogic1;

        public PlanetController(IPlanetLogic planetLogic)
        {
            planetLogic1 = planetLogic;
        }

        [HttpGet]
        public IEnumerable<Planets> GetPlanets()
        {
            var planets = planetLogic1.getAllPlanets();

            return planets;
        }

        [HttpGet("{id}")]
        public Planets Get(int id)
        {
            return planetLogic1.getPlanetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Planets planets)
        {
            planetLogic1.addPlanet(planets);
        }

        [HttpPut]
        public void Put([FromBody] Planets planets)
        {
            planetLogic1.renamePlanet(planets.PlanetId, planets.PlanetName);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            planetLogic1.deletePlanet(id);
        }

        [HttpGet("noncrudplanet/{pvpeventname}")]
        public Planets NonCrudMap(string pvpEventName)
        {
            return planetLogic1.NonCrudMap(pvpEventName);
        }
    }
}
