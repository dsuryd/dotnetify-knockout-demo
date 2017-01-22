"use strict";
var MenuItemVM = (function () {
    function MenuItemVM() {
    }
    // This function is bound to the back icon HTML click event.
    MenuItemVM.prototype.back = function () {
        window.history.back();
    };
    return MenuItemVM;
}());
exports.MenuItemVM = MenuItemVM;
