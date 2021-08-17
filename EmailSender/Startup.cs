using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmailSender.Startup))]
namespace EmailSender
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
