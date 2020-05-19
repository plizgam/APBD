using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class EFDbService : IDbService
    {
        private readonly DoctorDbContext _db;
        public EFDbService(DoctorDbContext db)
        {
            _db = db;
        }


        public IEnumerable<Doctor> GetDoctors()
        {
            return _db.Doctor.ToList();
        }



        public void AddDoctor(Doctor doctor)
        {
            _db.Add(doctor);
            _db.SaveChanges();
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _db.Update(doctor);
            _db.SaveChanges();
        }

        public void RemoveDoctor(Doctor doctor)
        {
            _db.Remove(doctor);
            _db.SaveChanges();
        }


    }
}
