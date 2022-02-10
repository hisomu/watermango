using PlantService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantService.Tests
{
    public class MockPlants
    {
        public static List<Plant> _plants = new List<Plant>()
        {
            new Plant {Id = Guid.NewGuid(), Name = "Cactus", ImageUrl = "https://static.wikia.nocookie.net/plantsvszombies/images/0/02/CactusPvZAS.png", State = State.Ok, LastUpdated = DateTime.UtcNow.AddHours(-6).AddMinutes(-23).AddSeconds(-25)},
            new Plant {Id = Guid.NewGuid(), Name = "Sunflower", ImageUrl = "https://static.wikia.nocookie.net/plantsvszombies/images/e/e6/1769830-plant_sunflower_smiling_thumb.png", State = State.Ok, LastUpdated = DateTime.UtcNow.AddHours(-6)},
            new Plant {Id = Guid.NewGuid(), Name = "Peashooter", ImageUrl = "https://static.wikia.nocookie.net/plantsvszombies/images/0/09/1769829-plant_peashooter_thumb.png", State = State.Ok, LastUpdated = DateTime.UtcNow.AddHours(-6)},
            new Plant {Id = Guid.NewGuid(), Name = "Chomper", ImageUrl = "https://static.wikia.nocookie.net/plantsvszombies/images/5/50/Chomper-hd.png", State = State.Ok, LastUpdated = DateTime.UtcNow.AddHours(-5)},
            new Plant {Id = Guid.NewGuid(), Name = "Squash", ImageUrl = "https://static.wikia.nocookie.net/plantsvszombies/images/4/42/Squash-hd.png", State = State.Ok, LastUpdated = DateTime.UtcNow.AddHours(-5) },
        };
    }
}
