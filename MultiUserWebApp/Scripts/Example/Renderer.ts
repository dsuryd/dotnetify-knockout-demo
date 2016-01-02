declare var PIXI: any;

module Renderer {

   export class Location {
      X: number;
      Y: number;
      Angle: number;

      constructor(iX: number, iY: number, iAngle: number) {
         this.X = iX;
         this.Y = iY;
         this.Angle = iAngle;
      }
   }

   // This class uses PIXI.js library to draw the bunnies.
   export class PixiRenderer {

      PEN_WIDTH: number = 500;
      PEN_HEIGHT: number = 400;

      // Send new location of a dragged bunny every 100 ms to control performance.
      UPDATE_RATE: number = 100;

      _stage: any;
      _renderer: any;
      _bunnies: any[] = [];
      _newPosition: any;
      _interval: number;

      constructor(iElementId: string) {

         // Initialize drawing area.
         this._renderer = PIXI.autoDetectRenderer(this.PEN_WIDTH, this.PEN_HEIGHT, { backgroundColor: 0x66FF99 });
         document.getElementById(iElementId).appendChild(this._renderer.view);
         this._stage = new PIXI.Container();

         this.startAnimation();
      }

      drawBunny(iId: number, iColor: { tint: number }, iDraggable: boolean, iWhereabout: Location, iMoveCallback: (moveTo: Location) => any) {

         // If bunny already exists, just update its state and leave.
         for (var i = 0; i < this._bunnies.length; i++) {
            var bunny = this._bunnies[i];
            if (bunny.Id == iId) {
               bunny.Sprite.position.x = iWhereabout.X;
               bunny.Sprite.position.y = iWhereabout.Y;
               bunny.Sprite.rotation = iWhereabout.Angle;
               bunny.Sprite.tint = iColor.tint;
               return;
            }
         }

         // Create the bunny sprite.
         var texture = PIXI.Texture.fromImage("/Content/bunny.png");
         var sprite: any = new PIXI.Sprite(texture);
         sprite.anchor.x = 0.5;
         sprite.anchor.y = 0.5;
         sprite.position.x = iWhereabout.X;
         sprite.position.y = iWhereabout.Y;
         sprite.rotation = iWhereabout.Angle;

         if (iColor.tint == undefined) {
            sprite.tint = (Math.random() * 0xFFFFFF).toFixed(1);
            iColor.tint = sprite.tint;
         }
         else
            sprite.tint = iColor.tint;

         // A user can only drag his/her own bunny.
         if (iDraggable) {
            sprite.interactive = true;
            sprite.buttonMode = true;

            // Handle the bunny dragging event.
            function onDragStart(event) {
               this.data = event.data;
               this.alpha = 0.9;
               this.dragging = true;
            }

            function onDragEnd() {
               this.alpha = 1;
               this.dragging = false;
               this.data = null;
            }

            var updateRate = this.UPDATE_RATE;
            var self = this;
            function onDragMove() {
               if (this.dragging) {
                  this.rotation += 0.1;
                  self._newPosition = this.data.getLocalPosition(this.parent);
                  this.position.x = self._newPosition.x;
                  this.position.y = self._newPosition.y;

                  // Send the new drag location to the server.  We do it in an interval
                  // of 100 ms, assuming average network latency is that.
                  if (self._interval == undefined)
                     self._interval = setInterval(() => {
                        if (self._newPosition != null)
                           iMoveCallback(new Location(self._newPosition.x, self._newPosition.y, this.rotation.toFixed(1)));
                        self._newPosition = null;
                     }, updateRate);
               }
            }

            sprite
               // events for drag start
               .on('mousedown', onDragStart)
               .on('touchstart', onDragStart)
               // events for drag end
               .on('mouseup', onDragEnd)
               .on('mouseupoutside', onDragEnd)
               .on('touchend', onDragEnd)
               .on('touchendoutside', onDragEnd)
               // events for drag move
               .on('mousemove', onDragMove)
               .on('touchmove', onDragMove);
         }

         this._stage.addChild(sprite);
         this._bunnies.push({ Id: iId, Sprite: sprite });
      }

      eraseBunny(iId: number) {

         for (var i = 0; i < this._bunnies.length; i++) {
            var bunny = this._bunnies[i];
            if (bunny.Id == iId) {
               this._stage.removeChild(bunny.Sprite);
               this._bunnies.splice(i, 1);
               break;
            }
         }
      }

      startAnimation() {
         var stage = this._stage;
         var renderer = this._renderer;

         animate();
         function animate() {
            requestAnimationFrame(animate);
            renderer.render(stage);
         }
      }
   }
}

var gRenderer = new Renderer.PixiRenderer("PenArea");