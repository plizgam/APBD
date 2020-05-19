using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw11.Models;
using Cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {

        private readonly IDbService _context;

        public DoctorsController(IDbService context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetDoctors()
        {

            try
            {
                return Ok(_context.GetDoctors());
            }
            catch(Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }


        [Route("add")]
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            try
            {
                _context.AddDoctor(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }


        [Route("update")]
        [HttpPost]
        public IActionResult UpdateDoctor(Doctor doctor)
        {
            try
            {
                _context.UpdateDoctor(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }


        [HttpDelete]
        public IActionResult RemoveDoctor(Doctor doctor)
        {
            try
            {
                _context.RemoveDoctor(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
