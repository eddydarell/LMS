using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_Grupp4.Startup))]
namespace LMS_Grupp4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
