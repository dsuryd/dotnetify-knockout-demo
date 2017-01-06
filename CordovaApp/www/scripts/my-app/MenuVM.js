var MenuVM = (function () {
    function MenuVM() {
    }
    // This function is called by dotnetify on view model ready event.
    MenuVM.prototype.$ready = function () {
        // On back/forward navigation event, close the right drawer if it's opened.
        $(window).on("popstate", this.closeRightDrawer);
    };
    // This function is called by dotnetify on view model destroy event.
    MenuVM.prototype.$destroy = function () {
        $(window).off("popstate", this.closeRightDrawer);
    };
    // This function is called by dotnetify.router on completion of routing.
    MenuVM.prototype.onRouteExit = function (iPath) {
        // Animate the sliding transition to MenuItem view.
        $("#MainPage").velocity({ translateX: ['-50%', '0%'] }).velocity("reverse");
        $("#RightDrawer").show().velocity({ translateX: ['0%', '100%'] });
    };
    MenuVM.prototype.closeRightDrawer = function () {
        var rightDrawer = $("#RightDrawer");
        if (rightDrawer.is(':visible')) {
            $("#MainPage").velocity({ translateX: ['0%', '-50%'] });
            rightDrawer.velocity({ translateX: ['100%', '0%'] }, { complete: function () { rightDrawer.hide().empty(); } });
        }
    };
    return MenuVM;
}());
//# sourceMappingURL=MenuVM.js.map