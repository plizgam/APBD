using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cw3.Models;
using Cw3.DAL;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        public string ConnectionString = "Data source=db-mssql;Initial Catalog=s18536;Integrated Security=True";
        public readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = new List<Student>();
            
            using(var con = new SqlConnection(ConnectionString))
            using(var command = new SqlCommand())
            {
                command.Connection = con;
                command.CommandText =
                    "SELECT Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name, Enrollment.Semester FROM Student, Studies, Enrollment WHERE Student.IdEnrollment = Enrollment.IdEnrollment AND Enrollment.IdStudy = Studies.IdStudy";

                con.Open();

                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    students.Add(new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirtDate = Convert.ToDateTime(dr["BirthDate"].ToString()),
                        StudyName = dr["Name"].ToString(),
                        Semester = Convert.ToInt32(dr["Semester"].ToString())
                    });
                }

            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetSemestersFromStudentId(string id)
        {
            var enrollments = new List<Enrollment>();
            using (var con = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = con;
                command.CommandText = "SELECT Enrollment.Semester, Enrollment.StartDate, Studies.Name FROM Enrollment, Student, Studies WHERE Student.IdEnrollment = Enrollment.IdEnrollment AND Enrollment.IdStudy = Studies.IdStudy AND Student.IndexNumber=@idStudent";
                command.Parameters.AddWithValue("idStudent", id);

                con.Open();

                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    enrollments.Add(new Enrollment
                    {
                        Semester = Convert.ToInt32(dr["Semester"].ToString()),
                        StartDate = Convert.ToDateTime(dr["StartDate"].ToString()),
                        StudyName = dr["Name"].ToString()
                    });
                }

            }
            return Ok(enrollments);
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