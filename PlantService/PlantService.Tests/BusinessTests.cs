using Microsoft.AspNetCore.SignalR;
using Moq;
using PlantService.Business;
using PlantService.Data;
using PlantService.HubConfig;
using PlantService.Models;
using System;
using Xunit;

namespace PlantService.Tests
{
	public class BusinessTests
	{
		Mock<IHubClients> _mockClients;
		Mock<IHubContext<PlantHub>> _mockHub;

		public BusinessTests()
		{
			_mockClients = new Mock<IHubClients>();
			Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
			_mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);
			var hubContext = new Mock<IHubContext<PlantHub>>();

			_mockHub = new Mock<IHubContext<PlantHub>>();
			_mockHub.Setup(x => x.Clients).Returns(() => _mockClients.Object);
		}

		[Fact]
		public void UpdatedPlant_ShouldReturnSamePlantAndPushedToSignalR()
		{
			var plant = MockPlants._plants[0];

			var repo = new Mock<IPlantRepository>();
			repo.Setup(_ => _.Update(plant)).Returns(plant);			

			PlantBusiness business = new PlantBusiness(repo.Object, _mockHub.Object);

			var updatedPlant = business.Update(plant);

			Assert.NotNull(updatedPlant);
			_mockClients.Verify(clients => clients.All, Times.Once);
		}

		[Fact]
		public void WaterPlant_ShouldReturnCorrectState()
		{
			var id = Guid.NewGuid();
			var plant = MockPlants._plants[0];
			var mockUpdatedPlant = plant.Clone();
			mockUpdatedPlant.State = State.Watering;

			var repo = new Mock<IPlantRepository>();
			repo.Setup(_ => _.GetById(id)).Returns(plant);
			repo.Setup(_ => _.Update(It.IsAny<Plant>())).Returns(mockUpdatedPlant);

			PlantBusiness business = new PlantBusiness(repo.Object, _mockHub.Object);

			var updatedPlant = business.StartWatering(id);

			Assert.NotNull(updatedPlant);
			Assert.Equal(State.Watering, updatedPlant.State);
		}

		[Fact]
		public void WaterPlant_ShouldThrowError()
		{
			var id = Guid.NewGuid();
			var plant = MockPlants._plants[0].Clone();
			plant.State = State.Watering;

			var repo = new Mock<IPlantRepository>();
			repo.Setup(_ => _.GetById(id)).Returns(plant);
			repo.Setup(_ => _.Update(It.IsAny<Plant>())).Returns(It.IsAny<Plant>());

			PlantBusiness business = new PlantBusiness(repo.Object, _mockHub.Object);

			Assert.Throws<Exception>(() => business.StartWatering(id));
			repo.Verify(clients => clients.Update(It.IsAny<Plant>()), Times.Never);
		}

		[Fact]
		public void StopWaterPlant_ShouldReturnCorrectState()
		{
			var id = Guid.NewGuid();
			var plant = MockPlants._plants[0];
			var mockUpdatedPlant = plant.Clone();
			mockUpdatedPlant.State = State.Ok;

			var repo = new Mock<IPlantRepository>();
			repo.Setup(_ => _.GetById(id)).Returns(plant);
			repo.Setup(_ => _.Update(It.IsAny<Plant>())).Returns(mockUpdatedPlant);

			PlantBusiness business = new PlantBusiness(repo.Object, _mockHub.Object);

			var updatedPlant = business.StopWatering(id);

			Assert.NotNull(updatedPlant);
			Assert.Equal(State.Ok, updatedPlant.State);
		}

		[Fact]
		public void StopWaterPlant_ShouldThrowError()
		{
			var id = Guid.NewGuid();
			var plant = MockPlants._plants[0].Clone();
			plant.State = State.Watered;

			var repo = new Mock<IPlantRepository>();
			repo.Setup(_ => _.GetById(id)).Returns(plant);
			repo.Setup(_ => _.Update(It.IsAny<Plant>())).Returns(It.IsAny<Plant>());

			PlantBusiness business = new PlantBusiness(repo.Object, _mockHub.Object);

			Assert.Throws<Exception>(() => business.StopWatering(id));
			repo.Verify(clients => clients.Update(It.IsAny<Plant>()), Times.Never);
		}
	}
}
