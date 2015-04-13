using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FizzBuzzWebApplication.Startup))]
namespace FizzBuzzWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
