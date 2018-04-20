using Nifty.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nifty.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //IDataAccess dataAccess = DapperFactory.GetDataAccess();
            //PageModel pageModel = new PageModel()
            //{
            //    TableName = "test",
            //    PageIndex = 2,
            //    PageSize = 20
            //};
            //var result = dataAccess.QueryByPaging<Test>(pageModel);

            //IDataAccess dataAccess = DapperFactory.GetDataAccess();
            //string test = "select top 100 * from Test";
            //List<Test> result = dataAccess.QueryAsSql<Test, dynamic>(test, null);



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class Test
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}