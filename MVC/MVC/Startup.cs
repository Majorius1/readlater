using Microsoft.Owin;
using Owin;
using SimpleInjector;
using System.Net;

[assembly: OwinStartupAttribute(typeof(MVC.Startup))]
namespace MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Container container = new Container();
            //Configure Simple Injector
            ConfigureSimpleInjector(app, container);
			//Set TLS 1.2 security protocol since Twilio need it
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}
