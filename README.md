#&nbsp;![alt tag](http://dotnetify.net/content/images/greendot.png) dotNetify - bunny pen example

Example of using **[dotNetify](http://dotnetify.net)** to build an interactive, real-time, multi-user web application.  
See live demo here: **[Bunny Pen](http://dotnetify.net/index/BunnyPen)**.

This example extends the basic 2D web graphics rendering example seen in the **[Pixi.js](http://pixijs.com)** website to allow multiple users see each other's bunny sprite as they drag it around the rendering area.

The principal source code files:

- The HTML view:
 [/Views/BunnyPen.html](https://github.com/dsuryd/dotNetify-example-bunnypen/blob/master/MultiUserWebApp/Views/BunnyPen.html)
 
- The C# .NET view model:
[/BunnyPenVM.cs](https://github.com/dsuryd/dotNetify-example-bunnypen/blob/master/MultiUserWebApp/BunnyPenVM.cs)
- The C# .NET Multi-User Service Module:
[/Pen.cs](https://github.com/dsuryd/dotNetify-example-bunnypen/blob/master/MultiUserWebApp/Pen.cs)

- The TypeScript code-behind:
[/Scripts/Example/BunnyPen.ts](https://github.com/dsuryd/dotNetify-example-bunnypen/blob/master/MultiUserWebApp/Scripts/Example/BunnyPen.ts)
- The TypeScript rendering module:
[/Scripts/Example/Renderer.ts](https://github.com/dsuryd/dotNetify-example-bunnypen/blob/master/MultiUserWebApp/Scripts/Example/Renderer.ts)
