using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(seoWebApplication.Startup))]
namespace seoWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
