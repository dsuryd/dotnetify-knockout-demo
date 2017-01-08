// Base server URL used by menu item components to build absolute path for images,
// as well as the location of signalR hub.
var gServerUrl = "http://169.254.80.80:5500/";

require.config({
   baseUrl: "scripts",
   paths: {
      // DotNetify's dependencies.
      "jquery": "libs/jquery-1.11.3.min",
      "jquery-ui": "libs/jquery-ui-widget-1.11.4.min",
      "knockout": "libs/knockout-3.4.0",
      "ko-mapping": "libs/knockout.mapping-latest",
      "dotnetify": "libs/dotnetify",
      "dnf-router": "libs/dotnetify.router",
      "dnf-binder": "libs/dotnetify.binder",
      "path": "libs/path.min",
      "signalr": "libs/jquery.signalR-2.2.0.min",
      "signalr-hub": "dotnetify-hub",
      "offline": "offline",    

      // Layout styling libraries.
      "bootstrap": "libs/bootstrap.min",
      "tether": "libs/tether.min",
      "snap": "libs/snap.min",
      "text": "libs/text",

      // Animation library.
      "velocity": "libs/velocity.min",

      // Application-specific scripts.
      "components": "my-components/components",
      "IndexVM": "my-app/IndexVM",
      "MenuVM": "my-app/MenuVM",
      "MenuItemVM": "my-app/MenuItemVM"
   },
   shim: {
      "jquery": { exports: "$" },
      "knockout": { exports: "ko" },
      "path": { exports: "Path" },
      "dnf-router": ["path"],
      "signalr": { deps: ["jquery"], exports: "$.connection" },
      "signalr-hub": ["signalr"],
      "bootstrap": ["jquery", "tether"],
      "velocity": { deps: ["jquery"] },
      "components": ["snap", "bootstrap", "text"],
      "IndexVM": { deps: ["velocity", "components", "MenuVM", "MenuItemVM"] },
      "MenuVM": { deps: ["jquery", "velocity"] },
      "MenuItemVM": { deps: ["jquery", "velocity"] }
   }
});

// Set the location of the signalR hub.
require(['signalr-hub'], function () { $.connection.hub.url = gServerUrl + "signalr"; });

// This is needed by bootstrap.
require(['tether'], function (Tether) { window.Tether = Tether; });

require(['jquery', 'knockout', 'dotnetify', 'dnf-router', 'offline', 'IndexVM'], function ($) {
   $(function () {
      dotnetify.debug = true;

      // Set requireJs's base url to absolute path so the web components can be loaded correctly.
      require.config({
         baseUrl: cordova.file.applicationDirectory + "www/scripts"
      });

      // Override routed URL in mobile devices to point to local device path.
      dotnetify.router.overrideUrl = function (iUrl) {
         return cordova.file.applicationDirectory + "www/views" + iUrl + ".html"
      };

      // On initial load of the Index page, navigate to the Home section.
      // On ASP.NET web server, this is done by the controller, but must be done manually on Cordova.
      $("[data-vm='IndexVM'").one("ready", function () {
         $(".my-side-nav #Home").click();
      });

   });
});