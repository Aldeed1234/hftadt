using ALDEED_HFT_2021221.Models;
using ALDEED_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public class PlanetLogic : IPlanetLogic
    {
        IPlanetsRepository planetsRepository;
        IPlayerRepository playerRepository;
        IPvpEventRepository pvpEventRepository;

        //Dependency injection
        public PlanetLogic(IPlanetsRepository repository, IPlayerRepository playerRepository1, IPvpEventRepository pvpEventRepository1)
        {
            this.planetsRepository = repository;
            playerRepository = playerRepository1;
            pvpEventRepository = pvpEventRepository1;
        }
        public void addPlanet(Planets planet)
        {
            planetsRepository.addPlanet(planet);
        }

        public void deletePlanet(int id)
        {
            planetsRepository.removePlanet(id);
        }

        public IList<Planets> getAllPlanets()
        {
            return planetsRepository.GetAll().ToList();
        }

        public Planets getPlanetById(int id)
        {
            return planetsRepository.GetOne(id);
        }

        public void renamePlanet(int id, string newName)
        {
            planetsRepository.renamePlanet(id, newName);
        }

        //non-crud
        //on which planet did most players die?
        public Planets NonCrudMap(string PvpEventName)
        {
            IQueryable<PvpEvent> pvpEvents = pvpEventRepository.GetAll();
            PvpEvent pvpEvent = pvpEvents.Where(x => x.PvpEventName == PvpEventName).FirstOrDefault();
            ICollection<Players> players = playerRepository.GetAll().Where(x => x.PvpEventId == pvpEvent.PvpEventId).ToList();
            pvpEvent.Players = players;

            var groupByElimination = players.GroupBy(x => x.EliminatedOnPlanet_PlanetId);
            var mostDied = groupByElimination.OrderByDescending(x => x.Count()).Select(x => x.Key).First();
            Planets planet = planetsRepository.GetOne(Convert.ToInt32(mostDied));
            return planet;
        }
    }
}
