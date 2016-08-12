require.config({
   baseUrl: '/Scripts',
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
      "ratchet": "ratchet.min",
      "snap": "snap.min",
      "ko-ratchet": "knockout.ratchet.components"
   },
   shim: {
      "jquery": { exports: "$" },
      "knockout": { exports: "ko" },
      "path": { exports: "Path" },
      "dnf-router": ["path"],
      "signalr": { deps: ["jquery"], exports: "$.connection" },
      "signalr-hub": ["signalr"]
   }
});

require( ['jquery', 'knockout', 'dotnetify', 'dnf-router', 'dnf-binder', 'ratchet', 'snap', 'ko-ratchet'], function ( $ )
{
   $(function () {
      dotnetify.debug = true;
      $( "[data-vm]" ).one( "ready", function () { $( this ).css( "opacity", 1 ) } );

      var snapper = new Snap({ element: document.getElementById('MainPage') });
      $("#open-left").click(function () {snapper.open("left") });
   });
});