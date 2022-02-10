using Microsoft.AspNetCore.SignalR;
using Moq;
using PlantService.Business;
using PlantService.Data;
using PlantService.HubConfig;
using PlantService.Models;
using PlantService.Services;
using System;
using System.Linq;
using Xunit;

namespace PlantService.Tests
{
	public class WaterMeTests
	{
		Mock<IPlantBusiness> _business;

		public WaterMeTests()
		{
			_business = new Mock<IPlantBusiness>();
		}

		[Fact]
		public void WaterWorks_ShouldUpdateToCorrectState()
		{
			var plants = MockPlants._plants;

			_business.Setup(_ => _.GetAll()).Returns(plants);

			WaterWorks service = new WaterWorks(_business.Object);

			service.WaterMe(null);

			Assert.True(plants.Where(x => x.State == State.Dry).Count() == 3);
			Assert.True(plants.Where(x => x.State == State.Ok).Count() == 2);
		}
	}
}
