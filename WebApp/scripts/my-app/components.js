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

   // Full-length menu item.
   ko.components.register("my-menu-item", {
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
               <span class='media-object pull-left btn-round btn-xs' data-bind='css: icon'></span>\
               <span data-bind='html: caption'></span>\
            </a>\
         </li>\
         <li style='display: none'/>"
   });
}))

