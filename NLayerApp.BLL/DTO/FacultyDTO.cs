using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.DTO
{
    public class FacultyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QtyBudget { get; set; }
        public int QtyAll { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; }
        public FacultyDTO()
        {
            Applicants = new List<Applicant>();
        }
    }
}
