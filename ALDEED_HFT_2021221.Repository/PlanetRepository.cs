using ALDEED_HFT_2021221.Data;
using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class PlanetRepository : Repository<Planets>, IPlanetsRepository
    {
        public PlanetRepository(VideoGameDbContext videoGameDbContext) : base(videoGameDbContext)
        {

        }
        public void addPlanet(Planets planet)
        {
            videoGameDbContext.Planets.Add(planet);
            videoGameDbContext.SaveChanges();
        }

        public void removePlanet(int id)
        {
            videoGameDbContext.Planets.Remove(GetOne(id));
            videoGameDbContext.SaveChanges();
        }

        public void renamePlanet(int id, string newName)
        {
            var planet = GetOne(id);
            planet.PlanetName = newName;
            videoGameDbContext.SaveChanges();
        }

        public override Planets GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.PlanetId == id);
        }
    }
}
