var IndexVM = (function () {
    function IndexVM() {
    }
    IndexVM.prototype.$ready = function () {
        var vm = this;
        // Get any existing shopping cart data in local storage and send it to the server.
        localStorage.getItem("ShoppingCart");
        var cart = "[{ \"ItemId\": 1, \"Qty\": 1 }, { \"ItemId\": 2, \"Qty\": 3 } ]";
        vm.CartLocalData(cart);
        vm.$on(vm.CartLocalData, function (data) { return localStorage.setItem("ShoppingCart", data); });
    };
    // This function is called by dotnetify.router on start of routing.
    IndexVM.prototype.onRouteEnter = function (iPath, iTemplate) {
        var vm = this;
        if (vm.$element.css("opacity") == 1)
            vm.$element.css("opacity", 0);
    };
    // This function is called by dotnetify.router on completion of routing.
    IndexVM.prototype.onRouteExit = function (iPath) {
        // Do a fade-in transition on the new view.
        var vm = this;
        vm.$element.velocity({ opacity: 1 }, { duration: 500 });
    };
    return IndexVM;
}());
//# sourceMappingURL=IndexVM.js.map