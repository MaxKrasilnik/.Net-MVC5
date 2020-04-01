using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.Services
{
    public class ApplicantFacultyService : IApplicantFacultyService
    {
        IUnitOfWork Database { get; set; }

        public ApplicantFacultyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakeApplicant(ApplicantDTO applicantDto)
        {
            Faculty faculty = Database.Faculties.Get(applicantDto.FacultyId);

            // валидация
            if (faculty == null)
                throw new ValidationException("Факультет не найден", "");

            Applicant applicant = new Applicant
            {
                Surname=applicantDto.Surname,
                Name=applicantDto.Name,
                Patronym = applicantDto.Patronym,
                EmailApp = applicantDto.EmailApp,
                City = applicantDto.City,
                Region = applicantDto.Region,
                Education = applicantDto.Education,
                Math = applicantDto.Math,
                UkrLanguage = applicantDto.UkrLanguage,
                EngLanguage = applicantDto.EngLanguage,

                FacultyId =faculty.Id,
                Faculty=faculty
            };

            Database.Applicants.Create(applicant);
            Database.Save();
        }

        public void MakeFaculty(FacultyDTO facultyDTO)
        {
            // валидация
            if (facultyDTO == null)
                throw new ValidationException("Данные не корректны", "");

            Faculty faculty = new Faculty
            {
                Name=facultyDTO.Name,
                QtyAll=facultyDTO.QtyAll,
                QtyBudget=facultyDTO.QtyBudget
            };
            Database.Faculties.Create(faculty);
            Database.Save();
        }

        public IEnumerable<FacultyDTO> GetFaculties()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Faculty, FacultyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Faculty>, List<FacultyDTO>>(Database.Faculties.GetAll());
        }

        public IEnumerable<ApplicantDTO> GetApplicants()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Applicant, ApplicantDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Applicant>, List<ApplicantDTO>>(Database.Applicants.GetAll());
        }

        public FacultyDTO GetFaculty(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id факультета", "");
            var faculty = Database.Faculties.Get(id.Value);
            if (faculty == null)
                throw new ValidationException("Факультет не найден", "");

            FacultyDTO facultyDTO = new FacultyDTO { Id = faculty.Id, Name = faculty.Name, QtyAll = faculty.QtyAll, QtyBudget = faculty.QtyBudget, Applicants=faculty.Applicants };
            return facultyDTO;

        }

        public ApplicantDTO GetApplicant(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id абитуриента", "");
            var applicant = Database.Applicants.Get(id.Value);
            if (applicant == null)
                throw new ValidationException("Абитуриент не найден", "");

            return new ApplicantDTO { Id = applicant.Id, Surname=applicant.Surname,
                Name = applicant.Name, Patronym=applicant.Patronym,
                EmailApp = applicant.EmailApp,
                City=applicant.City, Region=applicant.Region, Education=applicant.Education,
                Math=applicant.Math, UkrLanguage=applicant.UkrLanguage,
                EngLanguage =applicant.EngLanguage, FacultyId=applicant.FacultyId, Faculty=applicant.Faculty };
        }

        public void UpdateFaculty(FacultyDTO facultyDTO)
        {
            Database.Faculties.Update(new Faculty()
            {
                Id=facultyDTO.Id,
                Name=facultyDTO.Name,
                QtyBudget=facultyDTO.QtyBudget,
                QtyAll=facultyDTO.QtyAll
            });
        }

        public void UpdateApplicant(ApplicantDTO applicantDTO)
        {
            Database.Applicants.Update(new Applicant()
            {
                Id = applicantDTO.Id,
                Surname = applicantDTO.Surname,
                Name = applicantDTO.Name,
                Patronym = applicantDTO.Patronym,
                EmailApp = applicantDTO.EmailApp,
                City = applicantDTO.City,
                Region = applicantDTO.Region,
                Education = applicantDTO.Education,
                Math = applicantDTO.Math,
                UkrLanguage = applicantDTO.UkrLanguage,
                EngLanguage = applicantDTO.EngLanguage,
                FacultyId = applicantDTO.FacultyId,
                Faculty = applicantDTO.Faculty
            });
        }

        public void DeleteFaculty(int id)
        {
            Database.Faculties.Delete(id);
            Database.Save();
        }

        public void DeleteApplicant(int id)
        {
            Database.Applicants.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
