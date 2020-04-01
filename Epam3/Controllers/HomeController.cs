using AutoMapper;
using Epam3.Models;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Epam3.Controllers
{
    public class HomeController : Controller
    {
        IApplicantFacultyService service;
        public HomeController(IApplicantFacultyService serv)
        {
            service = serv;
        }

        public ActionResult Index()
        {
            /*IEnumerable<ApplicantDTO> applicantDTOs = service.GetApplicants();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantDTO, ApplicantViewModel>()).CreateMapper();
            var applicants = mapper.Map<IEnumerable<ApplicantDTO>, List<ApplicantViewModel>>(applicantDTOs);
            return View(applicants);*/
            return View();
        }

        public ActionResult AmdinApplicant(int faculty, int page = 1)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantDTO, ApplicantViewModel>()).CreateMapper();
            List<ApplicantViewModel> allApplicants = mapper.Map<IEnumerable<ApplicantDTO>, List<ApplicantViewModel>>(service.GetApplicants());

            var applicants = allApplicants.Where(a => a.FacultyId == faculty);

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<ApplicantViewModel> applicantsPerPages = applicants.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = applicants.Count<ApplicantViewModel>() };
            ApplicantGetView ivm = new ApplicantGetView { PageInfo = pageInfo, Applicants = applicantsPerPages };
            return View(ivm);
        }

        public ActionResult AmdinFaculty(int page = 1)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FacultyDTO, FacultyViewModel>()).CreateMapper();
            var faculties = mapper.Map<IEnumerable<FacultyDTO>, List<FacultyViewModel>>(service.GetFaculties());

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<FacultyViewModel> facultiesPerPages = faculties.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = faculties.Count };
            FacultyGetView ivm = new FacultyGetView { PageInfo = pageInfo, Faculties = facultiesPerPages };
            return View(ivm);
        }


        public ActionResult AmdinFacultySort(int value)
        {
            int page = 1;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FacultyDTO, FacultyViewModel>()).CreateMapper();
            var faculties = mapper.Map<IEnumerable<FacultyDTO>, List<FacultyViewModel>>(service.GetFaculties());

            if (value == 1)
                faculties = faculties.OrderBy(f => f.Name).ToList();
            if (value == 2)
                faculties = faculties.OrderBy(f => f.QtyBudget).ToList();
            if (value == 3)
                faculties = faculties.OrderBy(f => f.QtyAll).ToList();

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<FacultyViewModel> facultiesPerPages = faculties.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = faculties.Count };
            FacultyGetView ivm = new FacultyGetView { PageInfo = pageInfo, Faculties = facultiesPerPages };
            return View("AmdinFaculty", ivm);
        }

        public ActionResult Contact()
        {
            IEnumerable<ApplicantDTO> applicantDTOs = service.GetApplicants();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantDTO, ApplicantViewModel>()).CreateMapper();
            var faculties = mapper.Map<IEnumerable<ApplicantDTO>, List<ApplicantViewModel>>(applicantDTOs);
            return View(faculties);
        }

        //Регистрация абитуриента
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        //

        public ActionResult CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFaculty(FacultyViewModel faculty)
        {
            if (ModelState.IsValid)
            {
                service.MakeFaculty(new FacultyDTO()
                {
                    Name = faculty.Name,
                    QtyBudget = faculty.QtyBudget,
                    QtyAll = faculty.QtyAll
                });
                return Content("<h2 style='text-align: center;'>Факультет добавлен</h2>");
            }

            return View();
        }

        public ActionResult CreateApplicant(int? facultyId)
        {
            if (facultyId == null)
            {
                var facultyList = service.GetFaculties();
                foreach (var f in facultyList)
                    if (f.Applicants.Count == 0)
                        facultyId = f.Id;

            }

            FacultyDTO facultyDTO = service.GetFaculty(facultyId);
            FacultyViewModel facultyViewModel = new FacultyViewModel()
            {
                Id = facultyDTO.Id,
                Name = facultyDTO.Name,
                QtyBudget = facultyDTO.QtyBudget,
                QtyAll = facultyDTO.QtyAll,
                Applicants = facultyDTO.Applicants
            };
            return View(facultyViewModel);
        }

        [HttpPost]
        public ActionResult CreateApplicant(ApplicantViewModel applicant)
        {
            if (ModelState.IsValid)
            {
                service.MakeApplicant(new ApplicantDTO()
                {
                    Surname = applicant.Surname,
                    Name = applicant.Name,
                    Patronym = applicant.Patronym,
                    EmailApp = applicant.EmailApp,
                    City = applicant.City,
                    Region = applicant.Region,
                    Education = applicant.Education,
                    Math = applicant.Math,
                    UkrLanguage = applicant.UkrLanguage,
                    EngLanguage = applicant.EngLanguage,

                    FacultyId = applicant.FacultyId
                });
                return Content("<h2 style='text-align: center;'>Абитуриент добавлен</h2>");
            }

            return View();
        }

        public ActionResult EditFaculty(int faculty)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<FacultyDTO, FacultyViewModel>()).CreateMapper();
            FacultyViewModel facultyEdit = mapper.Map<FacultyDTO, FacultyViewModel>(service.GetFaculty(faculty));
            return View(facultyEdit);
        }

        [HttpPost]
        public ActionResult EditFaculty(FacultyViewModel faculty)
        {
            service.UpdateFaculty(new FacultyDTO()
            {
                Id = faculty.Id,
                Name = faculty.Name,
                QtyBudget = faculty.QtyBudget,
                QtyAll = faculty.QtyAll
            });

            return Content("<div style='text-align: center;'><h2>Изменения сохранены успешно</h2></div>");
        }

        public ActionResult EditApplicant(int applicantId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantDTO, ApplicantViewModel>()).CreateMapper();
            ApplicantViewModel applicantEdit = mapper.Map<ApplicantDTO, ApplicantViewModel>(service.GetApplicant(applicantId));
            return View(applicantEdit);
        }

        [HttpPost]
        public ActionResult EditApplicant(ApplicantViewModel applicant)
        {
            service.UpdateApplicant(new ApplicantDTO()
            {
                Id = applicant.Id,
                Surname = applicant.Surname,
                Name = applicant.Name,
                Patronym = applicant.Patronym,
                EmailApp = applicant.EmailApp,
                City = applicant.City,
                Region = applicant.Region,
                Education = applicant.Education,
                Math = applicant.Math,
                UkrLanguage = applicant.UkrLanguage,
                EngLanguage = applicant.EngLanguage,
                FacultyId = applicant.FacultyId,
                Faculty = applicant.Faculty
            });

            return Content("<div style='text-align: center;'><h2>Изменения сохранены успешно</h2></div>");
        }

        public ActionResult AllInfoApplicant(int applicantId)
        {

            ApplicantDTO applicantDto = service.GetApplicant(applicantId);
            FacultyDTO facultyDTO = service.GetFaculty(applicantDto.FacultyId);
            ApplicantViewModel applicantViewModel = new ApplicantViewModel()
            {
                Id = applicantDto.Id,
                Surname = applicantDto.Surname,
                Name = applicantDto.Name,
                Patronym = applicantDto.Patronym,
                EmailApp = applicantDto.EmailApp,
                City = applicantDto.City,
                Region = applicantDto.Region,
                Education = applicantDto.Education,
                Math = applicantDto.Math,
                UkrLanguage = applicantDto.UkrLanguage,
                EngLanguage = applicantDto.EngLanguage,

                FacultyId = applicantDto.FacultyId,
                Faculty = new Faculty() { Id = facultyDTO.Id, Name = facultyDTO.Name, QtyBudget = facultyDTO.QtyBudget, QtyAll = facultyDTO.QtyAll, Applicants = facultyDTO.Applicants }
            };
            return View(applicantViewModel);
        }

        public ActionResult DeleteFaculty(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDTO faculty = service.GetFaculty(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(new FacultyViewModel() { Id = faculty.Id, Name = faculty.Name, QtyAll = faculty.QtyAll, QtyBudget = faculty.QtyBudget, Applicants = faculty.Applicants });
        }

        [HttpPost]
        public ActionResult DeleteFacultyConfirmed(FacultyViewModel faculty)
        {
            service.DeleteFaculty(faculty.Id);
            return Content("<h2>Факультет удален</h2>");
        }

        public ActionResult DeleteApplicant(int? applicantId)
        {
            if (applicantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantDTO applicantDTO = service.GetApplicant(applicantId);
            if (applicantDTO == null)
            {
                return HttpNotFound();
            }
            return View(new ApplicantViewModel()
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

        [HttpPost]
        public ActionResult DeleteApplicantConfirmed(ApplicantViewModel applicant)
        {
            service.DeleteApplicant(applicant.Id);
            return Content("<h2>Абитуриент удален</h2>");
        }

        public float AverageRating(int math, int ukr, int eng)
        {
            return (math + ukr + eng) / 3;
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}