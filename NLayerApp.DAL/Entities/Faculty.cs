using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Entities
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QtyBudget { get; set; }
        public int QtyAll { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; }
        public Faculty()
        {
            Applicants = new List<Applicant>();
        }
    }
}
