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
    public class PlayerController
    {
        IPlayerLogic playerLogic;

        public PlayerController(IPlayerLogic playerLogic)
        {
            this.playerLogic = playerLogic;
        }

        [HttpGet]
        public IEnumerable<Players> GetPlayers()
        {
            var players = playerLogic.getAllPlayers();
            return players;
        }

        [HttpGet("{id}")]
        public Players Get(int id)
        {
            return playerLogic.getPlayerById(id);
        }

        [HttpPost]
        public void Post([FromBody] Players player)
        {
            playerLogic.addPlayer(player);
        }

        [HttpPut]       //------------------------------------------------------------------
        public void Put([FromBody] Players player)
        {
            playerLogic.changeKilledOrNot(player.PlayerId, player.IsEliminated, Convert.ToInt32(player.EliminatedOnPlanet_PlanetId));
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            playerLogic.removePlayer(id);
        }

        [HttpGet("whowonthispvpevent/{pvpEventId}")]
        public Players whoWonThisPvpEvent(int pvpEventId)
        {
            return playerLogic.whoWonThisPvpEvent(pvpEventId);
        }
    }
}
