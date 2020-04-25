using Kolokwium1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Services
{
    public class SqlServerDbService : IClinicDbService
    {
        private const string connectionString = "Data source=db-mssql;Initial Catalog=s18536;Integrated Security=True";


        public List<Animal> GetAnimals()
        {
            var animalsList = new List<Animal>();


            using (var con = new SqlConnection(connectionString))
            using (var comm = new SqlCommand())
            {
                comm.Connection = con;
                comm.CommandText = "SELECT Animal.Name, Animal.Type, Animal.AdmissionDate, Owner.LastName " +
                                   "FROM Animal, Owner " +
                                   "WHERE Animal.IdOwner = Owner.IdOwner";

                con.Open();

                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    var animal = new Animal
                    {
                        Name = reader["Name"].ToString(),
                        Type = reader["Type"].ToString(),
                        AdmissionDate = Convert.ToDateTime(reader["AdmissionDate"]),
                        LastNameOfOwner = reader["LastName"].ToString()
                    };

                    animalsList.Add(animal);
                }
            }

            return animalsList;
        }


        public void AddAnimalWithProcedures(Animal newAnimal, IEnumerable<Procedure> procedures)
        {
            using (var con = new SqlConnection(connectionString))
            using (var comm = new SqlCommand())
            {
                comm.Connection = con;

                con.Open();
                var transaction = con.BeginTransaction();
                comm.Transaction = transaction;

                try
                {
                    comm.CommandText = "INSERT INTO Animal(Name, Type, AdmissionDate, IdOwner) " +
                                       "VALUES(@name, @type, @date, @idOwner); SELECT SCOPE_IDENTITY()";

                    comm.Parameters.AddWithValue("name", newAnimal.Name);
                    comm.Parameters.AddWithValue("type", newAnimal.Type);
                    comm.Parameters.AddWithValue("date", newAnimal.AdmissionDate);
                    comm.Parameters.AddWithValue("idOwner", newAnimal.IdOwner);

                    var idAnimal = Convert.ToInt32(comm.ExecuteScalar());

                    if (procedures != null)
                        foreach (var procedure in procedures)
                        {

                            comm.Parameters.Clear();
                            comm.CommandText = "INSERT INTO Procedure_Animal " +
                                               "VALUES(@idProcedure, @idAnimal, @date)";

                            comm.Parameters.AddWithValue("idProcedure", procedure.IdProcedure);
                            comm.Parameters.AddWithValue("idAnimal", idAnimal);
                            comm.Parameters.AddWithValue("date", DateTime.Today);

                            comm.ExecuteNonQuery();
                        }



                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                }

            }
        }



    }
}
