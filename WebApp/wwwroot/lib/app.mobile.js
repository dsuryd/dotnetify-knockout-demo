// Base server URL used by menu item components to build absolute path for images.
// In cordova app, this should be set to specific server address.
//gServerUrl = "http://169.254.80.80:5500/"; // For Android emulator.
gServerUrl = "http://192.168.96.1:5500/"; // For iOS simulator on my machine.

// Global variables required by various modules.
$ = jQuery = require("jquery");
Tether = require("tether"); 
Snap = require("snapjs");

// Set the location of the signalR hub to our server.
require("signalr");
require("signalr-hub");
$.connection.hub.url = gServerUrl + "signalr";

var dotnetify = require("dotnetify");
dotnetify.debug = true;

require("dotnetify.router");
require("bootstrap");
require("velocity-animate");
require("offline.js");
require("my-components/components.js");

IndexVM = require("my-app/IndexVM.js").IndexVM;
MenuItemVM = require("my-app/MenuItemVM.js").MenuItemVM;
MenuVM = require("my-app/MenuVM.js").MenuVM;

// Override routed URL in mobile devices to point to local device path.
dotnetify.router.overrideUrl = function (iUrl) {
   return cordova.file.applicationDirectory + "www/views" + iUrl + ".html";
};

// When the Cordova is fully loaded...
document.addEventListener("deviceready", function () {

   // Prevent 300ms tap delay in mobile devices.
   var fastclick = require("fastclick");
   fastclick.attach(document.body);

   // If iOS, account for the device status bar.
   if (device && device.platform == "iOS") {
      document.body.style.marginTop = "20px";
      document.body.style.height = "calc(100% - 20px)";
   }
}, false);

$(function () {
   // On initial load of the Index page, navigate to the Home section.
   // On ASP.NET web server, this is done by the controller, but must be done manually on Cordova.
   $("[data-vm='IndexVM']").one("ready", function () {
      $(".my-side-nav #Home").click();
   });
});