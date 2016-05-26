using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FosterUniteForum.Startup))]
namespace FosterUniteForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
