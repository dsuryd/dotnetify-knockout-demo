class IndexVM {

   // This function is called by dotnetify.router on start of routing.
   onRouteEnter(iPath: string, iTemplate: any) {
      var vm: any = this;
      if (vm.$element.css("opacity") == 1)
         vm.$element.css("opacity", 0);
   }

   // This function is called by dotnetify.router on completion of routing.
   onRouteExit(iPath: string) {
      // Do a fade-in transition on the new view.
      var vm: any = this;
      vm.$element.velocity({ opacity: 1 }, { duration: 500 });
   }
}