using System.Web.Mvc;

namespace Alkoshop.Areas.Employee
{
    public class EmployeeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Employee";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Employee_default",
                "Employee/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index",AreaName="Employee", id = UrlParameter.Optional },
                namespaces: new[] {"Alkoshop.Areas.Employee.Controllers"}
            );
        }
    }
}