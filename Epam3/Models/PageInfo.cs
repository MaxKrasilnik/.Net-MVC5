﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam3.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class ApplicantGetView
    {
        public IEnumerable<ApplicantViewModel> Applicants { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class FacultyGetView
    {
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}