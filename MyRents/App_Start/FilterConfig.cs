using System.Web;
using System.Web.Mvc;

namespace MyRents
{
    public class FilterConfig
    {
        // This class is used to create GlobalFilters
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // This filter redirects the user to an error page when the action throws an exception
            filters.Add(new HandleErrorAttribute());

            // adding Authorize filter globally
            filters.Add(new AuthorizeAttribute());

            // Adding filter to use only HTTPS (secure) URL
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
