using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FosterUniteForum.Controllers
{
    public class HelperController : Controller
    {
        public ActionResult ForumIntro()
        {
            return View();
        }

        public ActionResult PaymentIntro()
        {
            return View();
        }
    }
}