using NLayerApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.DTO
{
    public class ApplicantDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronym { get; set; }
        public string EmailApp { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Education { get; set; }
        public int Math { get; set; }
        public int UkrLanguage { get; set; }
        public int EngLanguage { get; set; }

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }

}
