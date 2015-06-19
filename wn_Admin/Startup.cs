using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wn_Admin.Startup))]
namespace wn_Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
