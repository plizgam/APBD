using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Kolokwium1.Models;
using Kolokwium1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private IClinicDbService _dbService;

        public AnimalsController(IClinicDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetAnimals([FromQuery]string sortBy)
        {


            IEnumerable<Animal> animalsList;

            try
            {
                animalsList = _dbService.GetAnimals();
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Database error. Details:" + ex);
            }


            if (sortBy == null)
                sortBy = "";

            switch (sortBy.ToLower())
            {
                case "name":
                    animalsList = animalsList.OrderBy(x => x.Name);
                    break;
                case "animaltype":
                    animalsList = animalsList.OrderBy(x => x.Type);
                    break;
                case "lastnameofowner":
                    animalsList = animalsList.OrderBy(x => x.LastNameOfOwner);
                    break;
                case "dateofadmission":
                    animalsList = animalsList.OrderBy(x => x.AdmissionDate);
                    break;
                case "":
                    animalsList = animalsList.OrderByDescending(x => x.AdmissionDate);
                    break;
                default:
                    return StatusCode(400);
            }


            return Ok(animalsList.Select(x => new { x.Name, x.Type, x.AdmissionDate, x.LastNameOfOwner }));
        }

        [HttpPost]
        public IActionResult AddAnimal(AnimalWithProcedures request)
        {
            try
            {
                _dbService.AddAnimalWithProcedures(request.animal, request.procedures);
            }
            catch (Exception ex)
            {
                return StatusCode(404);
            }



            return Ok();
        }
    }
}