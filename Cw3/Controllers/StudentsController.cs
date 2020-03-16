using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cw3.Models;
using Cw3.DAL;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        public readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }




        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            //Pobranie rekordu z bazy danych o podanym id.
            var student = new Student { IdStudent = id };

            //Aktualizacja rekordu
            student.FirstName = "Weronika";

            //Zapis SaveChanges()

            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            //Usunięcie rekordu z bazy danych o podanym id.

            //Zapis SaveChanges()

            return Ok("Usuwanie dokończone");
        }
        
    }
}