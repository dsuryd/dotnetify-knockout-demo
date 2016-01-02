var MultiUserVM = (function () {
    function MultiUserVM() {
    }
    MultiUserVM.prototype.$ready = function () {
        var _this = this;
        var vm = this;
        this.drawMyBunny();
        this.drawOtherBunnies();
        vm.$on(vm.OtherBunnies, function () { return _this.drawOtherBunnies(); });
    };
    MultiUserVM.prototype.drawMyBunny = function () {
        var vm = this;
        var position = new Coordinate(vm.MyBunny.Position.X(), vm.MyBunny.Position.Y(), vm.MyBunny.Position.Angle());
        var tint = null;
        gPixiRenderer.addBunny(vm.MyBunny.Id(), tint, true, position, function (iPos) { return vm.MoveTo(iPos); });
        vm.MyBunny.Tint(tint);
    };
    MultiUserVM.prototype.drawOtherBunnies = function () {
        var vm = this;
        if (vm.OtherBunnies == null)
            return;
        var bunnies = vm.OtherBunnies();
        for (var idx in bunnies) {
            var bunny = bunnies[idx];
            if (bunny.Removed())
                gPixiRenderer.removeBunny(bunny.Id());
            else {
                var position = new Coordinate(bunny.Position.X(), bunny.Position.Y(), bunny.Position.Angle());
                gPixiRenderer.addBunny(bunny.Id(), bunny.Tint(), false, position, null);
            }
        }
    };
    return MultiUserVM;
})();
