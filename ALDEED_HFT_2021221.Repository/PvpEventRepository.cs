using ALDEED_HFT_2021221.Data;
using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class PvpEventRepository : Repository<PvpEvent>, IPvpEventRepository
    {
        public PvpEventRepository(VideoGameDbContext videoGameDbContext) : base(videoGameDbContext)
        {

        }
        public void addPvpEvent(PvpEvent pvpEvent)
        {
            videoGameDbContext.PvpEvent.Add(pvpEvent);
            videoGameDbContext.SaveChanges();
        }

        public void removePvpEvent(int id)
        {
            videoGameDbContext.PvpEvent.Remove(GetOne(id));
            videoGameDbContext.SaveChanges();
        }

        public void renamePvpEvent(int id, string newName)
        {
            var season = GetOne(id);
            season.PvpEventName = newName;
            videoGameDbContext.SaveChanges();
        }

        public override PvpEvent GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.PvpEventId == id);
        }
    }
}
