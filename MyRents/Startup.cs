using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(MyRents.Startup))]
namespace MyRents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


        }

    }
}
