using Kolokwium1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Services
{
    public interface IClinicDbService
    {
        List<Animal> GetAnimals();
        void AddAnimalWithProcedures(Animal newAnimal, IEnumerable<Procedure> procedures);
    }
}
