using ALDEED_HFT_2021221.Data;
using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class LocationRepository : Repository<Locations>, ILocationRepository
    {
        public LocationRepository(VideoGameDbContext videoGameDbContext) : base(videoGameDbContext)
        {

        }
        public void addLocation(Locations location)
        {
            videoGameDbContext.Locations.Add(location);
            videoGameDbContext.SaveChanges();
        }

        public void removeLocation(int id)
        {
            videoGameDbContext.Locations.Remove(GetOne(id));
            videoGameDbContext.SaveChanges();
        }

        public void changeLocation(int id, string newLocationName)
        {
            var location = GetOne(id);
            location.LocationName = newLocationName;
            videoGameDbContext.SaveChanges();
        }

        public override Locations GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.LocationId == id);
        }
    }
}
