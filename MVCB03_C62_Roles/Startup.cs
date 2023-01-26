using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCB03_C62_Roles.Startup))]
namespace MVCB03_C62_Roles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
