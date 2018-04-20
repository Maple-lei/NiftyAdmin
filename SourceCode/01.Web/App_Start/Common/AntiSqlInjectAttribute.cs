using Nifty.Utility;
using System.Web.Mvc;

namespace Nifty.Web
{
    public class AntiSqlInjectAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var p in actionParameters)
            {
                if (p.ParameterType == typeof(string))
                {
                    if (filterContext.ActionParameters[p.ParameterName] != null)
                    {
                        filterContext.ActionParameters[p.ParameterName] = StringHelper.FilterSql(filterContext.ActionParameters[p.ParameterName].ToString());
                    }
                }
            }
        }
    }
}