using System.Web.Mvc;

namespace FosterUniteForum.Areas.FosterUniteForum
{
    public class ForumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FosterUniteForum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Forum_default",
                "Forum/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FosterUniteForum.Areas.FosterUniteForum.Controllers" }
            );
        }
    }
}