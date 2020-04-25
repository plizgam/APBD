using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kolokwium1.Models
{

    public class Animal
    {
        public int IdAnimal { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [JsonPropertyName("AnimalType")]
        public string Type { get; set; }


        [Required]
        [JsonPropertyName("DateOfAdmission")]
        public DateTime AdmissionDate { get; set; }


        [Required]
        public int IdOwner { get; set; }


        public string LastNameOfOwner { get; set; }

    }
}
