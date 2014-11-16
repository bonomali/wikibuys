using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(seoWebApplication.Admin.Startup))]
namespace seoWebApplication.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
