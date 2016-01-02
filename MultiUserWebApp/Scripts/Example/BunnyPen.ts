// This is the Typescript module that receives data coming from the server-side 
// view model and calls the PIXI rendering engine to draw the bunnies.
// *** NOTE: 
// Notice that this Typescript module can access the property values of the 
// server view model, courtesy of dotNetify's MVVM abstraction over SignalR.
class BunnyPenVM {

   // This $ready method gets called after initial data from the server view model is received.
   $ready() {
      var vm: any = this;

      // Draw the bunnies asynchronously.
      setTimeout(() => {
         this.drawMyBunny();
         this.drawOtherBunnies();
      }, 1);

      // Use built-in function $on to listen to view model property changed events and act accordingly.
      vm.$on(vm.OtherBunnies, () => this.drawOtherBunnies());
      vm.$on(vm.DepartedBunnyIds, () => this.eraseOtherBunnies());
   }

   drawMyBunny() {
      var vm: any = this;
      if (vm.MyBunny == null)
         return;

      var color = { tint: null };
      var position = new Renderer.Location(vm.MyBunny.Whereabout.X(), vm.MyBunny.Whereabout.Y(), vm.MyBunny.Whereabout.Angle());
      gRenderer.drawBunny(vm.MyBunny.Id(), color, true, position, (iPos) => vm.MoveTo(iPos));

      // Send my bunny tint back to the server view model.
      vm.MyBunnyTint(color.tint);
   }

   drawOtherBunnies() {
      var vm: any = this;
      if (vm.OtherBunnies == null)
         return;

      var bunnies: any[] = vm.OtherBunnies();
      for (var i = 0; i < bunnies.length; i++) {
         var bunny = bunnies[i];
         var position = new Renderer.Location(bunny.Whereabout.X(), bunny.Whereabout.Y(), bunny.Whereabout.Angle());
         gRenderer.drawBunny(bunny.Id(), { tint: bunny.Tint() }, false, position, null);
      }
   }

   eraseOtherBunnies() {
      var vm: any = this;
      if (vm.DepartedBunnyIds == null)
         return;

      var bunnyIds: number[] = vm.DepartedBunnyIds();
      for (var i = 0; i < bunnyIds.length; i++)
         gRenderer.eraseBunny(bunnyIds[i]);
   }
}