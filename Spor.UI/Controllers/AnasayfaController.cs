using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spor.UI.Controllers
{
    [Authorize]
    public class AnasayfaController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}