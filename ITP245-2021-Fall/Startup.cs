using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITP245_2021_Fall.Startup))]
namespace ITP245_2021_Fall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
