using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Models
{
    public class AnimalWithProcedures
    {
        [Required]
        public Animal animal { get; set; }

        public IEnumerable<Procedure> procedures { get; set; }
    }
}
