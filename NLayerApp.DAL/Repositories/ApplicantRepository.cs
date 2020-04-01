using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Repositories
{
    public class ApplicantRepository : IRepository<Applicant>
    {
        private ApplicantFacultyContext db;

        public ApplicantRepository(ApplicantFacultyContext context)
        {
            this.db = context;
        }

        public IEnumerable<Applicant> GetAll()
        {
            return db.Applicants.Include(a=>a.Faculty);
        }

        public Applicant Get(int id)
        {
            return db.Applicants.Find(id);
        }

        public void Create(Applicant applicant)
        {
            db.Applicants.Add(applicant);
        }

        public void Update(Applicant applicant)
        {
            db.Entry(applicant).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Applicant> Find(Func<Applicant, Boolean> predicate)
        {
            return db.Applicants.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            if (applicant != null)
                db.Applicants.Remove(applicant);
        }
    }
}
