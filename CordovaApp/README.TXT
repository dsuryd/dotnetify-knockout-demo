CORDOVAAPP PROJECT NOTE
-----------------------

WebPack Bundling:

All the required Javascript files are bundled into scripts/bundle.mobile.min.js; the configuration file is at WebApp/webpack.mobile.config.js.
To rebundle, open the Task Runner Explorer window and run "config:mobile".

Running on Android Emulator:

- Install Visual Studio Emulator for Android.  You will need a Hyper-V system (if you have Intel I5/I7, enable it from the BIOS).
- Bind your IIS Express to http://169.254.80.80:5500.  You do this by going inside the .vs hidden folder in the project's root,
  edit .vs/config/applicationhost.config, find the site "WebApp" and add the binding below.

              <site name="WebApp" id="2">
                <bindings>
                    <binding protocol="http" bindingInformation="*:5500:localhost" />
                    <binding protocol="http" bindingInformation="*:5500:169.254.80.80" />
                </bindings>
            </site>

   Launch the WebApp and test the website is running on "http://169.254.80.80:5500/".

- In WebApp/lib/app.mobile.js, set gServerUrl to "http://169.254.80.80:5500/".
- In CordovaApp/www/index.html, include the IP address in both http and ws protocols to the "Content-Security-Policy" meta tag.
- Execute webpack bundling for "config:mobile" from Task Runner Explorer window.  It will update the bundle.mobile.min.js in CordovaApp project.
- Start debugging on a VS Emulator.

