using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEBNHOM10.Areas.Admin.Models.Authentication
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context.HttpContext.Session.GetString("TaiKhoan") + context.HttpContext.Session.GetInt32("Admin").ToString());
            if (context.HttpContext.Session.GetString("TaiKhoan") == null && context.HttpContext.Session.GetInt32("Admin") == null)
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller","Access" },
                    {"Action","Login" }
                });
            }
            else if (context.HttpContext.Session.GetString("TaiKhoan") != null && context.HttpContext.Session.GetInt32("Admin") == 0)
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller","Home" },
                    {"Action","Index" }
                });
            }
        }
    }
}
