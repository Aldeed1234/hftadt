using ALDEED_HFT_2021221.Data;
using ALDEED_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Repository
{
    public class PlayerRepository : Repository<Players>, IPlayerRepository
    {
        public PlayerRepository(VideoGameDbContext videoGameDbContext) : base(videoGameDbContext)
        {

        }
        public void changeKilledOrNot(int id, bool newStatus, int killedOnPlanetId)
        {
            var player = GetOne(id);
            player.IsEliminated = newStatus;
            player.EliminatedOnPlanet_PlanetId = killedOnPlanetId;
            videoGameDbContext.SaveChanges();
        }

        public void addPlayer(Players newPlayer)
        {
            videoGameDbContext.Players.Add(newPlayer);
            videoGameDbContext.SaveChanges();
        }

        public void removePlayer(int id)
        {
            videoGameDbContext.Players.Remove(GetOne(id));
            videoGameDbContext.SaveChanges();
        }

        public override Players GetOne(int id)
        {
            return GetAll().SingleOrDefault(x => x.PlayerId == id);
        }
    }
}
