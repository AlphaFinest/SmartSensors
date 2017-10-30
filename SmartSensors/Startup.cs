using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSensors.Startup))]
namespace SmartSensors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
