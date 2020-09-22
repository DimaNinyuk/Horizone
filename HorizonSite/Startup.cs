using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HorizonSite.Startup))]
namespace HorizonSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
