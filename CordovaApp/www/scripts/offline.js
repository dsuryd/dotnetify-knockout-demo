require(['jquery', 'dotnetify'], function ($) {

   // Enable offline mode.
   dotnetify.offline = true;
   dotnetify.offlineGetItem = function (key) { return sessionStorage.getItem(key) };
   dotnetify.offlineSetItem = function (key, data) { sessionStorage.setItem(key, data) };

   // Display offline mode whenever signalR connection isn't established.
   $.connection.hub.stateChanged(function (state) {
      if (state.newState != 1)
         $(".offline").show();
      else
         $(".offline").hide();
   });

   // Initial cached view model data for the offline mode.
   sessionStorage.setItem("IndexVM", '{"CartLocalData":"","SideNav":[{"Route":{"TemplateId":"Home","Path":"","RedirectRoot":null},"Caption":"Home","Icon":"fa fa-home btn-warning"},{"Route":{"TemplateId":"Menu","Path":"menu","RedirectRoot":null},"Caption":"Menu","Icon":"fa fa-list-alt btn-primary"},{"Route":{"TemplateId":"Account","Path":"account","RedirectRoot":null},"Caption":"Account","Icon":"fa fa-user btn-positive"}],"CartRoute":{"TemplateId":"Cart","Path":"cart","RedirectRoot":null},"RoutingState":{"Templates":[{"Id":"Home","Root":null,"UrlPattern":"","ViewUrl":"/home","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Menu","Root":null,"UrlPattern":"menu(/:tab)","ViewUrl":"/menu","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Account","Root":null,"UrlPattern":"account","ViewUrl":"/account","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Login","Root":null,"UrlPattern":"login","ViewUrl":"/login","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Cart","Root":null,"UrlPattern":"cart","ViewUrl":"/shopping-cart","JSModuleUrl":null,"Target":"MainPage"}],"Root":"app","Active":null,"Origin":null},"UserName":"Dicky Suryadi"}');
   sessionStorage.setItem("HomeVM", '{"RoutingState":null,"PageTitle":"My Fake App","Slogan":"Fresh meal delivered to your doorstep","MenuCaption":"See Our Menu","HowItWorksTitle":"How It Works","HowItWorksText":"It\'s as easy as 1, 2, 3.  You hungry, you tap what you want, we deliver it to you!","MenuRoute":{"TemplateId":null,"Path":"menu","RedirectRoot":"app"},"CartRoute":{"TemplateId":null,"Path":"cart","RedirectRoot":"app"},"OrderCount":0}');

});