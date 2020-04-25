using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Models
{
    public class Procedure
    {

        [Required]
        public int IdProcedure { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
