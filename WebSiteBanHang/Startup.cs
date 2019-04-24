using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteBanHang.Startup))]
namespace WebSiteBanHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
