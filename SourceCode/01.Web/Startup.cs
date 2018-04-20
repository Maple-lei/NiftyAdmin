using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nifty.Web.Startup))]
namespace Nifty.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
