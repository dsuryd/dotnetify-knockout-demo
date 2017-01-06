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
(function (factory)
{
   if (typeof define === 'function' && define['amd']) {
      define(['knockout'], factory);
   }
   else {
      factory(ko);
   }
}
(function (ko)
{
   // Full-length navigation item.
   ko.components.register('my-nav-item', {
      viewModel: function (params)
      {
         for (prop in params)
            this[prop] = params[prop];
      },
      template: { require: 'text!my-components/component-my-nav-item.html' }
   });

   // Full-length header bar.
   ko.components.register('my-header-bar', {
      viewModel: function (params)
      {
         for (prop in params)
            this[prop] = params[prop];

         this.openSideNav = function () {
            // Open the side navigation menu using the Snap library.
            var snapContent = $(".snap-content");
            var snapper = snapContent.data("snap");
            if (snapper == null) {
               snapper = new Snap({ element: snapContent.get(0), disable: 'right' });
               snapContent.data("snap", snapper);

               // Set up all nav links on the side menu to close the menu when clicked.
               $(".my-side-nav a").click(function () { console.log('test'); snapper.close() });
            }
            snapper.open("left");
         };
      },
      template: { require: 'text!my-components/component-my-header-bar.html' }
   });

   // Shopping cart.
   ko.components.register('my-cart-icon', {
      viewModel: function (params)
      {
         for (prop in params)
            this[prop] = params[prop];
      },
      template: { require: 'text!my-components/component-my-cart-icon.html' }
   });

   // Menu item.
   ko.components.register('my-menu', {
      viewModel: function (params)
      {
         for (prop in params)
            this[prop] = params[prop];
      },
      template: { require: 'text!my-components/component-my-menu.html' }
   });

   // Menu item.
   ko.components.register('my-menu-item', {
      viewModel: function (params)
      {
         for (prop in params)
            this[prop] = params[prop];
      },
      template: { require: 'text!my-components/component-my-menu-item.html' }
   });

   // Tab panel with slider to indicate tab selection.
   ko.components.register('my-tab-panel', {
      viewModel: function (params)
      {
         this.selected = ko.isObservable(params.selected) ? params.selected : ko.observable(params.selected);
      },
      template: { require: 'text!my-components/component-my-tab-panel.html' }
   });

   // Tab.
   ko.components.register('my-tab', {
      viewModel: function (params)
      {
         var self = this;
         for (prop in params)
            self[prop] = params[prop];

         self.onClick = function (data, event)
         {
            var tabPanel = $(event.target).closest('.tab-panel').get(0);
            ko.contextFor(tabPanel).$data.selected(self.id);
            return true;
         };
      },
      template: { require: 'text!my-components/component-my-tab.html' }
   });

   // Toaster.
   ko.components.register('my-toaster', {
      viewModel: function (params)
      {
         var self = this;
         self.state = ko.observable(null);
         self.timeout = 2000;

         for (prop in params)
            self[prop] = params[prop];

         self.show = function (vm)
         {
            if (self.state() !== null) {
               self.state('show');
               setTimeout(function () { self.state('') }, self.timeout);
            }
            else
               self.state('');
         };
      },
      template: { require: 'text!my-components/component-my-toaster.html' }
   });

}))

