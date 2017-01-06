// Base server URL used by menu item components to build absolute path for images.
// In cordova app, this should be set to specific server address.
var gServerUrl = "";

require.config({
   baseUrl: '/scripts',
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
      "signalr-hub": "/signalr/hubs?",

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
      "MenuItemVM": "my-app/MenuItemVM",
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

// This is needed by bootstrap.
require(['tether'], function (Tether) { window.Tether = Tether; });

require(['jquery', 'knockout', 'dotnetify', 'dnf-router', 'dnf-binder', 'IndexVM'], function ($) {
   $(function () {
      dotnetify.debug = true;
   });
});