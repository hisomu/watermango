using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantService.Business;
using PlantService.Models;
using System;
using System.Collections.Generic;

namespace PlantService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private IPlantBusiness _plantbiz;

        public PlantController(IPlantBusiness plantbiz)
        {
            _plantbiz = plantbiz;
        }

        [HttpGet]
        public List<Plant> GetAll()
        {
            return _plantbiz.GetAll();
        }

        [HttpPost("{id}/start")]
        public IActionResult StartWatering(Guid id)
        {
            try
            {
                Plant plant = _plantbiz.StartWatering(id);
                if (plant == null)
                {
                    return NotFound();
                }
                return StatusCode(200, plant);

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpPost("{id}/stop")]
        public IActionResult StopWatering(Guid id)
        {
            try
            {
                Plant plant = _plantbiz.StopWatering(id);
                if (plant == null)
                {
                    return NotFound();
                }
                return StatusCode(200, plant);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
