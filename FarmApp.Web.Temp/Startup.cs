using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmApp.Web.Temp.Startup))]
namespace FarmApp.Web.Temp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
