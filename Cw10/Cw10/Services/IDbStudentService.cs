using Cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public interface IDbStudentService
    {
        public IEnumerable<Student> GetStudents();
        public void ModifyStudent(Student student);
        public void RemoveStudent(Student student);
        public void EnrollStudent(Student student);
        public void PromoteStudents(Enrollment enrollment);
    }
}
