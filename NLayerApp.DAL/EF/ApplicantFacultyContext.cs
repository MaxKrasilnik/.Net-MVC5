using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.EF
{
    public class ApplicantFacultyContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        static ApplicantFacultyContext()
        {
            Database.SetInitializer<ApplicantFacultyContext>(new ApplicantFacultyDbInitializer());
        }
        public ApplicantFacultyContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
