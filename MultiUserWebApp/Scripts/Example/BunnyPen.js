// This is the Typescript module that receives data coming from the server-side 
// view model and calls the PIXI rendering engine to draw the bunnies.
// *** NOTE: 
// Notice that this Typescript module can access the property values of the 
// server view model, courtesy of dotNetify's MVVM abstraction over SignalR.
var BunnyPenVM = (function () {
    function BunnyPenVM() {
    }
    // This $ready method gets called after initial data from the server view model is received.
    BunnyPenVM.prototype.$ready = function () {
        var _this = this;
        var vm = this;
        // Draw the bunnies asynchronously.
        setTimeout(function () {
            _this.drawMyBunny();
            _this.drawOtherBunnies();
        }, 1);
        // Use built-in function $on to listen to view model property changed events and act accordingly.
        vm.$on(vm.OtherBunnies, function () { return _this.drawOtherBunnies(); });
        vm.$on(vm.DepartedBunnyIds, function () { return _this.eraseOtherBunnies(); });
    };
    BunnyPenVM.prototype.drawMyBunny = function () {
        var vm = this;
        if (vm.MyBunny == null)
            return;
        var color = { tint: null };
        var position = new Renderer.Location(vm.MyBunny.Whereabout.X(), vm.MyBunny.Whereabout.Y(), vm.MyBunny.Whereabout.Angle());
        gRenderer.drawBunny(vm.MyBunny.Id(), color, true, position, function (iPos) { return vm.MoveTo(iPos); });
        // Send my bunny tint back to the server view model.
        vm.MyBunnyTint(color.tint);
    };
    BunnyPenVM.prototype.drawOtherBunnies = function () {
        var vm = this;
        if (vm.OtherBunnies == null)
            return;
        var bunnies = vm.OtherBunnies();
        for (var i = 0; i < bunnies.length; i++) {
            var bunny = bunnies[i];
            var position = new Renderer.Location(bunny.Whereabout.X(), bunny.Whereabout.Y(), bunny.Whereabout.Angle());
            gRenderer.drawBunny(bunny.Id(), { tint: bunny.Tint() }, false, position, null);
        }
    };
    BunnyPenVM.prototype.eraseOtherBunnies = function () {
        var vm = this;
        if (vm.DepartedBunnyIds == null)
            return;
        var bunnyIds = vm.DepartedBunnyIds();
        for (var i = 0; i < bunnyIds.length; i++)
            gRenderer.eraseBunny(bunnyIds[i]);
    };
    return BunnyPenVM;
})();
//# sourceMappingURL=BunnyPen.js.map