"use strict";

var webpack = require("webpack");
module.exports = {
   entry: "./Scripts/app.js",
   output: {
      filename: "./Scripts/bundle.js"
   },
   resolve: {
      modules: ["src", "node_modules"],
      alias: {
         "ko-mapping": "dotnetify/dist/knockout.mapping-latest",
         "dotnetify.router": "dotnetify/dist/dotnetify.router",
         "dotnetify-hub": "dotnetify/dist/dotnetify-hub"
      }
   },
   plugins: [
      new webpack.optimize.UglifyJsPlugin({ minimize: true })
   ]
};