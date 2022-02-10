using Microsoft.AspNetCore.SignalR;
using PlantService.Data;
using PlantService.HubConfig;
using PlantService.Models;
using System;
using System.Collections.Generic;

namespace PlantService.Business
{
    public class PlantBusiness : IPlantBusiness
    {
        private IPlantRepository _repo;
        private readonly IHubContext<PlantHub> _hubContext;

        public PlantBusiness(IPlantRepository plantRepo, IHubContext<PlantHub> hubContext)
        {
            _repo = plantRepo;
            _hubContext = hubContext;
        }
                
        public List<Plant> GetAll()
        {
            return _repo.GetAll();
        }

        public Plant GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public Plant StartWatering(Guid id)
        {
            Plant plant = GetById(id);

            // Set plant status to watering
            if (plant != null)
            {
                if (!(plant.State == State.Watering || plant.State == State.Watered))
                {
                    plant.State = State.Watering;
                    plant = Update(plant);
                }
                else
                {
                    throw new Exception("Cannot water plant at this time");
                }
            }

            return plant;
        }

        public Plant StopWatering(Guid id)
        {
            Plant plant = GetById(id);

            // Set plant status to resting
            if (plant != null)
            {
                if (plant.State != State.Watered)
                {
                    plant.State = State.Ok;
                    plant = Update(plant);
                }
                else
                {
                    throw new Exception("Cannot stop watering plant at this time");
                }
            }

            return plant;
        }

        public Plant Update(Plant plant)
        {
            var plantUpdated = _repo.Update(plant);

            _hubContext.Clients.All.SendAsync("UpdatedPlant", plantUpdated);

            return plantUpdated;
        }
    }
}
