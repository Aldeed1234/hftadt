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
    public class PvpEventsController
    {
        IPvpEventLogic pvpEventLogic1;

        public PvpEventsController(IPvpEventLogic pvpEventLogic)
        {
            this.pvpEventLogic1 = pvpEventLogic;
        }

        [HttpGet]
        public IEnumerable<PvpEvent> GetPvpEvents()
        {
            var pvpEvents = pvpEventLogic1.getAllPvpEvents();
            return pvpEvents;
        }

        [HttpGet("{id}")]
        public PvpEvent Get(int id)
        {
            return pvpEventLogic1.getPvpEventById(id);
        }

        [HttpPost]
        public void Post([FromBody] PvpEvent pvpEvent)
        {
            pvpEventLogic1.newPvpEvent(pvpEvent);
        }

        [HttpPut]
        public void Put([FromBody] PvpEvent pvpEvent)
        {
            pvpEventLogic1.changePvpEventName(pvpEvent.PvpEventId, pvpEvent.PvpEventName);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            pvpEventLogic1.removePvpEvent(id);
        }

        [HttpGet("whichpvpeventfirstinthislocation/{locationName}")]
        public PvpEvent whichPvpEventInThisLocation(string locationName)
        {
            return pvpEventLogic1.whichPvpEventInThisLocation(locationName);
        }

        [HttpGet("whichpvpeventwonbythisplayer/{playerId}")]
        public PvpEvent whichPvpEventWonByThisPlayer(int playerId)
        {
            return pvpEventLogic1.whichPvpEventWonByThisPlayer(playerId);
        }
    }
}
