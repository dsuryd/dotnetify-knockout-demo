require(['jquery', 'dotnetify'], function ($) {

   // Enable offline mode.
   dotnetify.offline = true;
   dotnetify.offlineCacheFn = function (key, data) {
      if (typeof data === "undefined")
         return sessionStorage.getItem(key);
      sessionStorage.setItem(key, data);
   }

   // Display offline mode whenever the offline event is raised by dotNetify on the document level.
   $(document).on("offline", function (event, isOffline) {
      if (isOffline)
         $(".offline").show();
      else
         $(".offline").hide();
   });

   // Initial cached view model data for the offline mode.
   sessionStorage.setItem("IndexVM", '{"CartLocalData":"","SideNav":[{"Route":{"TemplateId":"Home","Path":"","RedirectRoot":null},"Caption":"Home","Icon":"fa fa-home btn-warning"},{"Route":{"TemplateId":"Menu","Path":"menu","RedirectRoot":null},"Caption":"Menu","Icon":"fa fa-list-alt btn-primary"},{"Route":{"TemplateId":"Account","Path":"account","RedirectRoot":null},"Caption":"Account","Icon":"fa fa-user btn-positive"}],"CartRoute":{"TemplateId":"Cart","Path":"cart","RedirectRoot":null},"RoutingState":{"Templates":[{"Id":"Home","Root":null,"UrlPattern":"","ViewUrl":"/home","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Menu","Root":null,"UrlPattern":"menu(/:tab)","ViewUrl":"/menu","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Account","Root":null,"UrlPattern":"account","ViewUrl":"/account","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Login","Root":null,"UrlPattern":"login","ViewUrl":"/login","JSModuleUrl":null,"Target":"MainPage"},{"Id":"Cart","Root":null,"UrlPattern":"cart","ViewUrl":"/shopping-cart","JSModuleUrl":null,"Target":"MainPage"}],"Root":"app","Active":null,"Origin":null},"UserName":"Dicky Suryadi"}');
   sessionStorage.setItem("HomeVM", '{"RoutingState":null,"PageTitle":"My Fake App","Slogan":"Fresh meal delivered to your doorstep","MenuCaption":"See Our Menu","HowItWorksTitle":"How It Works","HowItWorksText":"It\'s as easy as 1, 2, 3.  You hungry, you tap what you want, we deliver it to you!","MenuRoute":{"TemplateId":null,"Path":"menu","RedirectRoot":"app"},"CartRoute":{"TemplateId":null,"Path":"cart","RedirectRoot":"app"},"OrderCount":0}');
   sessionStorage.setItem("ShoppingCartVM", '{"RoutingState":null,"PageTitle":"My Fake App","ReviewOrderCaption":"Review your order","RemoveCaption":"Remove","QtyCaption":"QTY","PlaceOrderCaption":"Place Order","ShoppingCartItems":[],"DisablePlaceOrder":true,"PlaceOrderCommand":null,"OrderPlacedToaster":"Thank you for your order!","OrderPlacedToasterTrigger":0}');
   sessionStorage.setItem("MenuVM", '{"RoutingState":{"Templates":[{"Id":"MenuItem","Root":null,"UrlPattern":"item(/:id)","ViewUrl":"/menu-item","JSModuleUrl":null,"Target":"RightDrawer"}],"Root":"menu","Active":null,"Origin":"menu"},"PageTitle":"Daily Menu","BreakfastTabCaption":"Breakfast","LunchTabCaption":"Lunch","DinnerTabCaption":"Dinner","BreakfastMenu":[],"LunchMenu":[],"DinnerMenu":[],"ActiveTab":"tab-breakfast","CartRoute":{"TemplateId":null,"Path":"cart","RedirectRoot":"app"},"OrderCount":0}');
   sessionStorage.setItem("MenuItemVM", '{"RoutingState":{"Templates":null,"Root":null,"Active":null},"AddCaption":"Add","PageTitle":"","MenuItem":{"Id":0,"Name":"","Description":"","Price":"","ImageUrl":"","Route":null,"AddCommand":null,"ItemAdded":null}}');
});