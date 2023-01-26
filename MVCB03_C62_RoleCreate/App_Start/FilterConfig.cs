using System.Web;
using System.Web.Mvc;

namespace MVCB03_C62_RoleCreate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
