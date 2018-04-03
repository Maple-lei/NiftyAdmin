using Nifty.Model;
using Nifty.Model.SystemManagement;
using Nifty.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nifty.Web.Controllers
{
    public class App_UserController : Controller
    {
        public CommonService commonService = new CommonService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AntiSqlInject]
        public JsonResult ValidateLogin(string username, string password)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                string querySql = string.Format(" select * from App_User where LoginAccount = '{0}' and Password = '{1}'", username, password);

                List<App_User> lstUser = commonService.QueryAsSql<App_User, dynamic>(querySql, null);

                if (lstUser != null && lstUser.Count > 0)
                {
                    returnModel.Code = (int)ReturnCode.Success;
                    returnModel.Data = "";
                    returnModel.Message = "";
                }
                else
                {
                    returnModel.Code = (int)ReturnCode.NotFind;
                    returnModel.Data = "";
                    returnModel.Message = "";
                }

                return Json(returnModel);
            }
            catch (Exception ex)
            {
                returnModel.Code = (int)ReturnCode.Exception;
                returnModel.Data = "";
                returnModel.Message = ex.Message;

                return Json(returnModel);
            }
        }
    }
}