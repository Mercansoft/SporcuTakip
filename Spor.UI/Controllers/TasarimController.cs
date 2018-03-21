using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spor.UI.Controllers
{
    public class TasarimController : Controller
    {
        // GET: Tasarim
        public ActionResult _Layout()
        {
            return View();
        }
        public PartialViewResult _Menu() { return PartialView(); }
        public PartialViewResult _Menu2() { return PartialView(); }
    }
}