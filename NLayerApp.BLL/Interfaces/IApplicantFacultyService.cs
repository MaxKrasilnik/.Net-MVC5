using NLayerApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.Interfaces
{
    public interface IApplicantFacultyService
    {
        void MakeApplicant(ApplicantDTO applicantDto);
        void MakeFaculty(FacultyDTO facultyDTO);
        FacultyDTO GetFaculty(int? id);
        ApplicantDTO GetApplicant(int? id);
        IEnumerable<FacultyDTO> GetFaculties();
        IEnumerable<ApplicantDTO> GetApplicants();
        void UpdateFaculty(FacultyDTO facultyDTO);
        void UpdateApplicant(ApplicantDTO applicantDTO);
        void DeleteFaculty(int id);
        void DeleteApplicant(int id);
        void Dispose();
    }
}
