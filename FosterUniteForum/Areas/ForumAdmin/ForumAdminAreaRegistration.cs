using System.Web.Mvc;

namespace FosterUniteForum.Areas.ForumAdmin
{
    public class ForumAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ForumAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ForumAdmin_default",
                "ForumAdmin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FosterUniteForum.Areas.ForumAdmin.Controllers" }
            );
        }
    }
}