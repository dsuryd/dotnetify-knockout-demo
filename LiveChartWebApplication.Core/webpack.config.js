"use strict";

let webpack = require('webpack');

module.exports = {
   entry: "./wwwroot/lib/app.js",
   output: {
      filename: "wwwroot/lib/bundle.min.js"
   },
   resolve: {
      "modulesDirectories": ["node_modules", "wwwroot/js"],
      alias: {
         "jquery-ui": "jquery.ui.widget/jquery.ui.widget",
         "ko-mapping": "dotnetify/dist/knockout.mapping-latest",
         "signalr-hub": "dotnetify/dist/dotnetify-hub",
         "dotnetify.router": "dotnetify/dist/dotnetify.router",
         "path": "dotnetify/dist/path.min"
      }
   },
   plugins: [
    new webpack.optimize.UglifyJsPlugin({ minimize: true })
   ]
};