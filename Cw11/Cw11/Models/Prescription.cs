using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class Prescription
    {

        public Prescription()
        {
            PrescriptionMedicaments = new HashSet<PrescriptionMedicament>();
        }

        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
