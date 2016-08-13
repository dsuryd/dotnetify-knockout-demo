require.config({
   baseUrl: '/scripts',
   paths: {
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
      "tether": "tether.min",
      "bootstrap": "bootstrap.min",
      "snap": "snap.min",
      "my-components": "my-app/components"
   },
   shim: {
      "jquery": { exports: "$" },
      "knockout": { exports: "ko" },
      "path": { exports: "Path" },
      "dnf-router": ["path"],
      "signalr": { deps: ["jquery"], exports: "$.connection" },
      "signalr-hub": ["signalr"],
      "bootstrap": ["jquery", "tether"],
      "my-components": ["snap", "bootstrap"]
   }
});

require(['tether'], function (Tether) {
   // This is needed by bootstrap.
   window.Tether = Tether;
});

require(['jquery', 'knockout', 'dotnetify', 'dnf-router', 'dnf-binder', 'my-components'], function ($) {
   $(function () {
      dotnetify.debug = true;

      $( "[data-vm]" ).on( "ready", function ()
      {
         // On fresh view, find and init the designated button to open the side menu.
         var snapper = new Snap({ element: $( ".snap-content").get(0) });
         $(".my-side-menu-btn").click(function () { snapper.open("left") });

         setInterval(function () { $(".my-side-menu a").click(function () { snapper.close() }); }, 200);
      });

   });
});