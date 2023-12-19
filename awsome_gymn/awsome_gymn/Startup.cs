using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(awsome_gymn.Startup))]
namespace awsome_gymn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
