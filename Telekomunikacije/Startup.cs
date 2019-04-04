using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Telekomunikacije.Startup))]
namespace Telekomunikacije
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
