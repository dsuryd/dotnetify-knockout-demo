using Microsoft.Owin;
using Owin;
using DotNetify;

[assembly: OwinStartup(typeof(LiveChartWebApplication.OWINStartup))]

namespace LiveChartWebApplication
{
   public class OWINStartup
   {
      public void Configuration(IAppBuilder app)
      {
         app.MapSignalR();
         app.UseDotNetify();
      }
   }
}
