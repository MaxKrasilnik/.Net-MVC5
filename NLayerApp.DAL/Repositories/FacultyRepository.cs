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
    public class FacultyRepository : IRepository<Faculty>
    {
        private ApplicantFacultyContext db;

        public FacultyRepository(ApplicantFacultyContext context)
        {
            this.db = context;
        }

        public IEnumerable<Faculty> GetAll()
        {
            return db.Faculties.Include(f => f.Applicants);
        }

        public Faculty Get(int id)
        {
            return db.Faculties.Find(id);
        }

        public void Create(Faculty faculty)
        {
            db.Faculties.Add(faculty);
        }

        public void Update(Faculty faculty)
        {
            db.Entry(faculty).State = EntityState.Modified;
            db.SaveChanges();
            //using (ApplicantFacultyContext context = db)
            //{
            //    Faculty f = db.Faculties.Find(faculty.Id);
            //    f.Name = faculty.Name;
            //    f.QtyBudget = faculty.QtyBudget;
            //    f.QtyAll = faculty.QtyAll;
            //    context.SaveChanges();
            //}
        }

        public IEnumerable<Faculty> Find(Func<Faculty, Boolean> predicate)
        {
            return db.Faculties.Include(f => f.Applicants).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            if (faculty != null)
                db.Faculties.Remove(faculty);
        }
    }
}
