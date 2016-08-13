/* 
Copyright 2016 Dicky Suryadi

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
(function (factory) {
   if (typeof define === "function" && define["amd"]) {
      define(['knockout'], factory);
   }
   else {
      factory(ko);
   }
}
(function (ko) {

   // Full-length navigation item.
   ko.components.register("my-nav-item", {
      viewModel: function (params) {
         var self = this;
         self.icon = params.icon;
         self.url = params.url;
         self.caption = params.caption;
         self.nav = params.nav;  // Set to 'navigate-right' to add right arrow at the end.
      },
      template: "\
         <li class='table-view-cell media'>\
            <a data-bind='vmRoute: url, css: nav'>\
               <span class='media-object pull-left btn-round' data-bind='css: icon'></span>\
               <span data-bind='html: caption'></span>\
            </a>\
         </li>\
         <li style='display: none'/>"
   });

   // Full-length header bar.
   ko.components.register("my-header-bar", {
      viewModel: function (params) {
         var self = this;
         self.title = params.title;
      },
      template: "\
         <header class='bar bar-nav'>\
            <a class='my-side-nav-btn icon icon-bars pull-left'></a>\
            <a class='icon icon-refresh pull-right'></a>\
            <h1 data-bind='html: title' class='title'></h1>\
         </header>"
   });

   // Menu item.
   ko.components.register("my-menu-item", {
      viewModel: function (params) {
         var self = this;
         self.imageUrl = params.imageUrl;
         self.caption = params.caption;
         self.price = params.price;
      },
      template: "\
         <div class='col-lg-3 col-xs-6 col-sm-4'>\
            <div class='card'>\
               <img class='card-img-top' data-bind='attr: {src: imageUrl, alt: caption }'>\
               <div class='card-block'>\
                  <h4 class='card-title' data-bind='html: caption'></h4>\
                  <div data-bind='html: price' class='pull-left'></div>\
                  <a href='#' class='btn btn-primary pull-right'>+</a>\
               </div>\
            </div>\
         </div>"
   });
}))

