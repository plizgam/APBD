using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{

    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private string ConnectionString = "Data source=db-mssql;Initial Catalog=s18536;Integrated Security=True";
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Index(Student student)
        {
            return _service.Index(student);
        }

        [HttpPost("promotions")]
        public IActionResult promotions(EnrollmentRequest enrollment)
        {
            return _service.promotions(enrollment);
        }

    }
}