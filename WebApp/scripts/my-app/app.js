require.config({
   baseUrl: '/scripts/libs',
   paths: {
      // DotNetify's dependencies.
      "jquery": "jquery-1.11.3.min",
      "jquery-ui": "jquery-ui-widget-1.11.4.min",
      "knockout": "knockout-3.3.0",
      "ko-mapping": "knockout.mapping-latest",
      "dotnetify": "dotnetify",
      "dnf-router": "dotnetify.router",
      "dnf-binder": "dotnetify.binder",
      "path": "path.min",
      "signalr": "jquery.signalR-2.2.0.min",
      "signalr-hub": "/signalr/hubs?",

      // Layout styling libraries.
      "bootstrap": "bootstrap.min",
      "tether": "tether.min",
      "snap": "snap.min",

      // Animation library.
      "velocity": "velocity.min",

      // Application-specific scripts.
      "my-components": "/scripts/my-app/components",
      "IndexVM": "/scripts/my-app/IndexVM",
      "MenuVM": "/scripts/my-app/MenuVM",
      "MenuItemVM": "/scripts/my-app/MenuItemVM",
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
      "my-components": ["snap", "bootstrap"],
      "IndexVM": { deps: ["velocity", "my-components", "MenuVM", "MenuItemVM"] },
      "MenuVM": { deps: ["jquery", "velocity"] },
      "MenuItemVM": { deps: ["jquery", "velocity"] }
   }
});

require(['tether'], function (Tether) {
   // This is needed by bootstrap.
   window.Tether = Tether;
});

require(['jquery', 'knockout', 'dotnetify', 'dnf-router', 'dnf-binder', 'IndexVM'], function ($) {
   $(function () {
      dotnetify.debug = true;

      $("[data-vm]").on("ready", function () {
         // On a new view, set up the menu button to open the side menu, and
         // set up all nav links on the side menu to close the menu when clicked.
         var snapper = new Snap({ element: $(".snap-content").get(0), disable: 'right' });
         $(".my-side-nav-btn").click(function () { snapper.open("left") });
         $(".my-side-nav a").click(function () { snapper.close() });
      });

   });
});