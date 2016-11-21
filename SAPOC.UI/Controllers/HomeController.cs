using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAPOC.Repository.Common;
using SAPOC.Contract;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;


namespace SAPOC.UI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           
            return View();
        }        

    }
}
