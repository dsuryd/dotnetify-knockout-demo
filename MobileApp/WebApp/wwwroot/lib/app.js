// Base server URL used by menu item components to build absolute path for images.
// In cordova app, this should be set to specific server address.
gServerUrl = "";

// Global variables required by various modules.
$ = jQuery = require("jquery");
Tether = require("tether"); 
Snap = require("snapjs");  

require("dotnetify").debug = true;
require("dotnetify.router");
require("bootstrap");
require("velocity-animate");
require("offline.js");
require("my-components/components.js");

IndexVM = require("my-app/IndexVM.js").IndexVM;
MenuItemVM = require("my-app/MenuItemVM.js").MenuItemVM;
MenuVM = require("my-app/MenuVM.js").MenuVM;
