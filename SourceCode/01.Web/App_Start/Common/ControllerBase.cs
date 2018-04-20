using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nifty.Web
{
    public class ControllerBase : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
	}
}