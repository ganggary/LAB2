using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAB2.Startup))]
namespace LAB2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
