using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epam3.Startup))]
namespace Epam3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
