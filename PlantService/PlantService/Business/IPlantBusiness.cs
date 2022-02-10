using PlantService.Models;
using System;
using System.Collections.Generic;

namespace PlantService.Business
{
    public interface IPlantBusiness
    {
        /// <summary>
        /// Retrieve a list of plants
        /// </summary>
        /// <returns>List of plants</returns>
        List<Plant> GetAll();

        /// <summary>
        /// Get the plant by Id
        /// </summary>
        /// <param name="id">Id of the plant</param>
        /// <returns>The plant</returns>
        Plant GetById(Guid id);

        /// <summary>
        /// Start watering the plant
        /// </summary>
        /// <param name="id">Id of the plant that is being watered</param>
        /// <returns>Updated plant</returns>
        Plant StartWatering(Guid id);

        /// <summary>
        /// Stop watering the plant
        /// </summary>
        /// <param name="id">Id of the plant</param>
        /// <returns>Updated plant</returns>
        Plant StopWatering(Guid id);

        /// <summary>
        /// Updates the state of the plant and pushes the update via signalr
        /// </summary>
        /// <param name="id">Id of the plant</param>
        /// <returns>Updated plant</returns>
        Plant Update(Plant plant);
    }
}
