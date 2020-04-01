using Ninject.Modules;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam3.Util
{
    public class ApplicantFacultyModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicantFacultyService>().To<ApplicantFacultyService>();
        }
    }
}