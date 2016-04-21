using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarksonGroceries.Startup))]
namespace MarksonGroceries
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
