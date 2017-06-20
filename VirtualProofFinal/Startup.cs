using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualProofFinal.Startup))]
namespace VirtualProofFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
