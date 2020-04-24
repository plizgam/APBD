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
        IActionResult EnrollStudent(Student student);
        IActionResult PromoteStudents(EnrollmentRequest enrollment);
        bool checkIndex(string index);
        bool AccountExist(LoginRequestDto request);
        IActionResult LoginUpdate(LoginRequestDto request);
        IActionResult RegisterAccount(LoginRequestDto data);
    }
}
