CORDOVAAPP PROJECT NOTE
-----------------------

WebPack Bundling:

All the required Javascript files are bundled into scripts/bundle.mobile.min.js; the configuration file is at WebApp/webpack.mobile.config.js.  To rebundle, open the Task Runner Explorer window and run "config:mobile".

Running on Android Emulator:

- Install Visual Studio Emulator for Android.  You will need a Hyper-V system on Windows 10.  Follow this guide to enable it: https://docs.microsoft.com/en-us/virtualization/hyper-v-on-windows/quick-start/enable-hyper-v.

- Bind your IIS Express to an IP address the emulator can see.  For Android, it is 169.254.80.80.  Assuming you keep this project's default port 5500,  go inside the .vs hidden folder in the project's root,  edit .vs/config/applicationhost.config, find the site "WebApp" and add the binding below.

              <site name="WebApp" id="2">
                <bindings>
                    <binding protocol="http" bindingInformation="*:5500:localhost" />
                    <binding protocol="http" bindingInformation="*:5500:169.254.80.80" />
                </bindings>
            </site>

  Launch the WebApp and test the website shows up when you type "http://169.254.80.80:5500/" on the browser URL.

- In WebApp/lib/app.mobile.js, set gServerUrl to "http://169.254.80.80:5500/".

- In CordovaApp/www/index.html, include the IP address in both http and ws protocols to the "Content-Security-Policy" meta tag.

- Execute webpack bundling for "config:mobile" from Task Runner Explorer window.  It will update the bundle.mobile.min.js in CordovaApp project.

- Start Visual Studio debugging on a VS Emulator.

Running on iOS Simulator:

- You will need a Mac OS. Follow Microsoft guide at: https://taco.visualstudio.com/en-us/docs/ios-guide/

- Bind your IIS Express to an IP address your MAC OS can see.  Assuming you keep this project's default port 5500,  go inside the .vs hidden folder in the project's root,  edit .vs/config/applicationhost.config, find the site "WebApp" and add the binding below.

              <site name="WebApp" id="2">
                <bindings>
                    <binding protocol="http" bindingInformation="*:5500:localhost" />
                    <binding protocol="http" bindingInformation="*:5500:<your IP address>" />
                </bindings>
             </site>
            
   Launch the WebApp and test the website shows up when you type "http://<your IP address>:5500/" on the browser URL.

- In WebApp/lib/app.mobile.js, set gServerUrl to "http://<your IP address>:5500/".

- In CordovaApp/www/index.html, include the IP address in both http and ws protocols to the "Content-Security-Policy" meta tag.

- Execute webpack bundling for "config:mobile" from Task Runner Explorer window.  It will update the bundle.mobile.min.js in CordovaApp project.

- Start Visual Studio debugging on a iOS Simulator.
