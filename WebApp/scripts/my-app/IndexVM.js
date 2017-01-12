var IndexVM = (function () {
    function IndexVM() {
    }
    IndexVM.prototype.$ready = function () {
        var vm = this;
        // Get any existing shopping cart data in local storage and send it to the server.
        var cart = localStorage.getItem("ShoppingCart");
        vm.CartLocalData(cart);
        // When data on the server is changed, save it to the local storage.
        vm.$on(vm.CartLocalData, function (data) { return localStorage.setItem("ShoppingCart", data); });
    };
    // This function is called by dotnetify.router on completion of routing.
    IndexVM.prototype.onRouteExit = function (iPath) {
        var vm = this;
        // Do a fade-in transition on the new view.
        vm.$element.css("opacity", 0);
        vm.$element.velocity({ opacity: 1 }, { duration: 500 });
    };
    return IndexVM;
}());
//# sourceMappingURL=IndexVM.js.map