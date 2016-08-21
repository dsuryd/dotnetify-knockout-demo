/* 
Copyright 2016 Dicky Suryadi

Licensed under the Apache License, Version 2.0 (the 'License');
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an 'AS IS' BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
(function (factory) {
   if (typeof define === 'function' && define['amd']) {
      define(['knockout'], factory);
   }
   else {
      factory(ko);
   }
}
(function (ko) {

   // Full-length navigation item.
   ko.components.register('my-nav-item', {
      viewModel: function (params) {
         var self = this;
         self.icon = params.icon;
         self.url = params.url;
         self.caption = params.caption;
         self.nav = params.nav;  // Set to 'navigate-right' to add right arrow at the end.
      },
      template: { require: 'text!/scripts/my-app/component-my-nav-item.html' }
   });

   // Full-length header bar.
   ko.components.register('my-header-bar', {
      viewModel: function (params) {
         var self = this;
         self.title = params.title;
      },
      template: { require: 'text!/scripts/my-app/component-my-header-bar.html' }
   });

   // Menu item.
   ko.components.register('my-menu', {
      viewModel: function (params) {
         var self = this;
         self.menuItems = params.menuItems;
      },
      template: { require: 'text!/scripts/my-app/component-my-menu.html' }
   });

   // Menu item.
   ko.components.register('my-menu-item', {
      viewModel: function (params) {
         var self = this;
         self.imageUrl = params.imageUrl;
         self.name = params.name;
         self.price = params.price;
         self.route = params.route;
         self.add = params.add;
      },
      template: { require: 'text!/scripts/my-app/component-my-menu-item.html' }
   });

   // Tab panel with slider to indicate tab selection.
   ko.components.register('my-tab-panel', {
      viewModel: function (params) {
         var self = this;
         self.selected = ko.isObservable(params.selected) ? params.selected : ko.observable(params.selected);
      },
      template: { require: 'text!/scripts/my-app/component-my-tab-panel.html' }
   });

   // Tab.
   ko.components.register('my-tab', {
      viewModel: function (params) {
         var self = this;
         self.id = params.id;
         self.caption = params.caption;
         self.onClick = function (data, event) {
            var tabPanel = $(event.target).closest('.tab-panel').get(0);
            ko.contextFor(tabPanel).$data.selected(self.id);
            return true;
         };
      },
      template: { require: 'text!/scripts/my-app/component-my-tab.html' }
   });

}))

