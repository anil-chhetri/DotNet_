using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vivdly.Startup))]
namespace vivdly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
