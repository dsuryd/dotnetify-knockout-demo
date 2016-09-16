var IndexVM = (function () {
    function IndexVM() {
    }
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