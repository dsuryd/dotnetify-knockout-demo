export class IndexVM {

   $ready() {
      var vm: any = this;

      // Get any existing shopping cart data in local storage and send it to the server.
      var cart = localStorage.getItem("ShoppingCart");
      vm.CartLocalData(cart);

      // When data on the server is changed, save it to the local storage.
      vm.$on(vm.CartLocalData, data => localStorage.setItem("ShoppingCart", data));
   }

   // This function is called by dotnetify.router on completion of routing.
   onRouteExit(iPath: string) {
      var vm: any = this;

      // Do a fade-in transition on the new view.
      vm.$element.css("opacity", 0);
      vm.$element.velocity({ opacity: 1 }, { duration: 500 });
   }
}