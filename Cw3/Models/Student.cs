using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirtDate { get; set; }
        [Required]
        public string Studies { get; set; }
        public int Semester { get; set; }

    }
}
