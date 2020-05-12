using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.Models;
using Cw10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {

        private readonly IDbStudentService _context;

        public StudentController(IDbStudentService context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                return Ok(_context.GetStudents());
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost]
        public IActionResult ModifyStudent(Student student)
        {
            try
            {
                _context.ModifyStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpDelete]
        public IActionResult RemoveStudent(Student student)
        {
            try
            {
                _context.RemoveStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


        [Route("enroll")]
        [HttpPost]
        public IActionResult EnrollStudent(Student student)
        {
            try
            {
                _context.EnrollStudent(student);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [Route("promote")]
        [HttpPost]
        public IActionResult PromoteStudents(Enrollment enrollment)
        {
            try
            {
                _context.PromoteStudents(enrollment);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

    }
}