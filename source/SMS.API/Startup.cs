using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SMS.API.Startup))]

namespace SMS.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
