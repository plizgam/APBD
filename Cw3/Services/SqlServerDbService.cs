using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class SqlServerDbService : ControllerBase, IStudentDbService
    {

        public string ConnectionString = "Data source=db-mssql;Initial Catalog=s18536;Integrated Security=True";

        public IActionResult Index(Student student)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);

            int idEnrollment;
            var myEnrollment = new Enrollment();
            bool enrollmentExist;
            int IdStudy;


            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                comm.CommandText = "SELECT * FROM Studies WHERE Name=@studyName";
                comm.Parameters.AddWithValue("studyName", student.Studies);

                var reader = comm.ExecuteReader();

                if (!reader.Read())
                    return StatusCode(400);


                IdStudy = (int)reader["IdStudy"];

                reader.Close();
            }

            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                comm.CommandText = "SELECT TOP 1 * FROM Enrollment, Studies WHERE Studies.Name = @study AND Enrollment.IdStudy = Studies.IdStudy AND Enrollment.Semester = 1 ORDER BY StartDate DESC";
                comm.Parameters.AddWithValue("study", student.Studies);

                var reader = comm.ExecuteReader();
                enrollmentExist = reader.Read();


                if (enrollmentExist)
                    idEnrollment = (int)reader["IdEnrollment"];
                else
                    reader.Close();
            }

            if (!enrollmentExist)
                using (var con = new SqlConnection(ConnectionString))
                using (var comm = new SqlCommand("", con))
                {
                    con.Open();
                    var transaction = con.BeginTransaction("newEnrollment");

                    try
                    {
                        comm.Transaction = transaction;
                        comm.CommandText = "INSERT INTO Enrollment Values(1, @IdStudy, @StartDate)";

                        comm.Parameters.AddWithValue("IdStudy", IdStudy);
                        comm.Parameters.AddWithValue("StartDate", DateTime.Now);
                        comm.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();

                        return StatusCode(400);
                    }
                }


            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                comm.CommandText = "SELECT TOP 1 * FROM Enrollment, Studies WHERE Studies.Name = @studyM AND Enrollment.IdStudy = Studies.IdStudy AND Enrollment.Semester = 1 ORDER BY StartDate DESC";
                comm.Parameters.AddWithValue("studyM", student.Studies);
                var readerEnrollment = comm.ExecuteReader();
                readerEnrollment.Read();

                idEnrollment = (int)readerEnrollment["IdEnrollment"];
                readerEnrollment.Close();
            }


            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                var transaction = con.BeginTransaction("newStudent");
                try
                {

                    comm.Transaction = transaction;
                    comm.CommandText = "INSERT INTO Student VALUES(@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment)";

                    comm.Parameters.AddWithValue("IndexNumber", student.IndexNumber);
                    comm.Parameters.AddWithValue("FirstName", student.FirstName);
                    comm.Parameters.AddWithValue("LastName", student.LastName);
                    comm.Parameters.AddWithValue("BirthDate", student.BirtDate.ToShortDateString());
                    comm.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    comm.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();

                    return StatusCode(400);
                }
            }


            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                comm.CommandText = "SELECT * FROM Enrollment WHERE IdEnrollment = @id";
                comm.Parameters.AddWithValue("id", idEnrollment);
                var reader = comm.ExecuteReader();
                reader.Read();

                myEnrollment = new Enrollment
                {
                    IdEnrollment = idEnrollment,
                    Semester = (int)reader["Semester"],
                    StartDate = (DateTime)reader["StartDate"],
                    IdStudy = (int)reader["IdStudy"]
                };
                reader.Close();
            }



            return StatusCode(201, myEnrollment);
        }

        public IActionResult promotions(EnrollmentRequest enrollment)
        {
            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();

                comm.CommandText = "SELECT TOP 1 * FROM Enrollment, Studies WHERE Enrollment.Semester = @semester AND Studies.Name = @studyName AND Enrollment.IdStudy = Studies.IdStudy";
                comm.Parameters.AddWithValue("semester", enrollment.Semester);
                comm.Parameters.AddWithValue("studyName", enrollment.Studies);

                var reader = comm.ExecuteReader();

                if (!reader.Read())
                    return StatusCode(404);
                reader.Close();
            }


            var newEnrollment = new Enrollment();

            using (var con = new SqlConnection(ConnectionString))
            using (var comm = new SqlCommand("", con))
            {
                con.Open();
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "UpdateStudent";
                comm.Parameters.Add(new SqlParameter("@Semester", enrollment.Semester));
                comm.Parameters.Add(new SqlParameter("@Studies", enrollment.Studies));

                var reader = comm.ExecuteReader();
                reader.Read();

                newEnrollment = new Enrollment
                {
                    IdEnrollment = (int)reader["IdEnrollment"],
                    IdStudy = (int)reader["IdStudy"],
                    Semester = (int)reader["Semester"],
                    StartDate = (DateTime)reader["StartDate"]
                };

                reader.Close();
            }


            return StatusCode(201, newEnrollment);
        }
    }
}
