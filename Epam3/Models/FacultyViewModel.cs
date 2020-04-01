using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam3.Models
{
    public class FacultyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QtyBudget { get; set; }
        public int QtyAll { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; }
        public FacultyViewModel()
        {
            Applicants = new List<Applicant>();
        }
    }
}