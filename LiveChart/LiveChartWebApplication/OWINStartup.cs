using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LiveChartWebApplication.OWINStartup))]

namespace LiveChartWebApplication
{
    public class OWINStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
