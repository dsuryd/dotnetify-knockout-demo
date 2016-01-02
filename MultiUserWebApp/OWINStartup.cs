using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DynamicLINQWebApp.OWINStartup))]

namespace DynamicLINQWebApp
{
    public class OWINStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
