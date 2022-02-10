using Microsoft.Extensions.Hosting;
using PlantService.Business;
using PlantService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlantService.Services
{
    public class WaterWorks : IHostedService
    {
        private Timer _timer;
        private readonly IPlantBusiness _plantBiz;

        public WaterWorks(IPlantBusiness plantBiz)
        {
            _plantBiz = plantBiz;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // do a polling of 1 second
            _timer = new Timer(WaterMe, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Update state of watering a plant
        /// </summary>
        /// <param name="state"></param>
        public void WaterMe(object state)
        {
            _plantBiz.GetAll().ForEach(plant =>
            {
                var diff = (DateTime.UtcNow - plant.LastUpdated).TotalSeconds;

                // after 10 seconds of watering, update state to watered
                if (plant.State == State.Watering && diff >= 10)
                {
                    plant.State = State.Watered;
                    _plantBiz.Update(plant);
                }
                // after 30 seconds since watering, update state that the plant is ok
                else if (plant.State == State.Watered && diff >= 30)
                {
                    plant.State = State.Ok;
                    _plantBiz.Update(plant);
                }
                // after 6 hours seconds since watering, update state that the plant is dried up
                else if (plant.State == State.Ok && diff >= 21600)
                {
                    plant.State = State.Dry;
                    _plantBiz.Update(plant);
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            return Task.CompletedTask;
        }
    }
}
