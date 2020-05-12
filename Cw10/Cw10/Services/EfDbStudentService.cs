using Cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public class EfDbStudentService : IDbStudentService
    {
        private readonly StudentDbContext db;

        public EfDbStudentService(StudentDbContext context)
        {
            db = context;
        }


        public IEnumerable<Student> GetStudents()
        {
            return db.Student.ToList();
        }

        public void ModifyStudent(Student student)
        {
            db.Update(student);
            db.SaveChanges();
        }

        public void RemoveStudent(Student student)
        {
            db.Remove(student);
            db.SaveChanges();
        }


        public void EnrollStudent(Student student)
        {

            if (!db.Enrollment.Any(x => x.IdEnrollment == student.IdEnrollment))
            {

                var enroll = new Enrollment
                {
                    IdStudy = 1,
                    Semester = 1,
                    StartDate = DateTime.Now
                };

                db.Add(enroll);
                db.SaveChanges();

                student.IdEnrollment = enroll.IdEnrollment;
            }
               


            db.Add(student);
            db.SaveChanges();
        }

        public void PromoteStudents(Enrollment enrollment)
        {


            var newEnrollment = new Enrollment
            {
                Semester = enrollment.Semester + 1,
                StartDate = DateTime.Now
            };



            db.Add(newEnrollment);
            db.SaveChanges();


            foreach (var student in db.Student.Where(x => x.IdEnrollment == enrollment.IdEnrollment).ToList())
                student.IdEnrollment = newEnrollment.IdEnrollment;

            db.SaveChanges();
        }
    }
}
