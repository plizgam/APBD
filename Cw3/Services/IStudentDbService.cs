using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public interface IStudentDbService
    {
        IActionResult Index(Student student);
        IActionResult promotions(EnrollmentRequest enrollment);
    }
}
