using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.EF
{
    public class ApplicantFacultyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicantFacultyContext>
    {
        protected override void Seed(ApplicantFacultyContext db)
        {
            Applicant ap1 = new Applicant { Id = 1, Surname = "Иванов", Name = "Егор", Patronym = "Егорович", EmailApp = "egor@gmail.com", City = "Харьков", Region = "Харьковская область", Education = "Школа 1", Math = 150, UkrLanguage = 160, EngLanguage = 170 };
            Applicant ap2 = new Applicant { Id = 2, Surname = "Васильева", Name = "Мария", Patronym = "Васильевна", EmailApp = "maria@gmail.com", City = "Харьков", Region = "Харьковская область", Education = "Школа 2", Math = 160, UkrLanguage = 170, EngLanguage = 180 };
            Applicant ap3 = new Applicant { Id = 3, Surname = "Кузнецов", Name = "Олег", Patronym = "Олегович", EmailApp = "oleg@gmail.com", City = "Харьков", Region = "Харьковская область", Education = "Школа 3", Math = 140, UkrLanguage = 150, EngLanguage = 160 };
            Applicant ap4 = new Applicant { Id = 4, Surname = "Петрова", Name = "Ольга", Patronym = "Петровна", EmailApp = "olga@gmail.com", City = "Харьков", Region = "Харьковская область", Education = "Школа 4", Math = 120, UkrLanguage = 130, EngLanguage = 140 };

            db.Applicants.Add(ap1);
            db.Applicants.Add(ap2);
            db.Applicants.Add(ap3);
            db.Applicants.Add(ap4);

            db.Faculties.Add(new Faculty
            {
                Id = 1,
                Name = "Факультет компьютерных наук",
                QtyBudget=10,
                QtyAll =100,
                Applicants = new List<Applicant> { ap1, ap2, ap3 }
            });

            db.Faculties.Add(new Faculty
            {
                Id = 2,
                Name = "Факультет компьютерной инженерии и управления",
                QtyBudget = 20,
                QtyAll = 90,
                Applicants = new List<Applicant> { ap2, ap4 }
            });

            db.Faculties.Add(new Faculty
            {
                Id = 3,
                Name = "Факультет информационно-аналитических технологий",
                QtyBudget = 30,
                QtyAll = 80,
                Applicants = new List<Applicant> { ap3, ap4, ap1 }
            });

            db.SaveChanges();
        }
    }
}
