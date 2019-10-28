using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Awwsp.Startup))]
namespace Awwsp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
