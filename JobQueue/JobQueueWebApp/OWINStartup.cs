using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof( JobQueueWebApp.OWINStartup))]

namespace JobQueueWebApp
{
    public class OWINStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
