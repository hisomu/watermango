using PlantService.Models;
using System;
using System.Collections.Generic;

namespace PlantService.Data
{
    public interface IPlantRepository
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
        /// Updates the state of the plant
        /// </summary>
        /// <param name="id">Id of the plant</param>
        /// <returns>Updated plant</returns>
        Plant Update(Plant plant);
    }
}
