using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/Students")]
    public class StudentsController : ControllerBase
    {
        [HttpGet("getStudents")]
        public string GetStudents()
        {
            return "Nowak, Kowalski";
        }
    }
}