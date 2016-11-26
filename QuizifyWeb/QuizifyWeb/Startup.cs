using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizifyWeb.Startup))]
namespace QuizifyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}
