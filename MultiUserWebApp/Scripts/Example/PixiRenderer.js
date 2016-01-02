var Coordinate = (function () {
    function Coordinate(iX, iY, iAngle) {
        this.X = iX;
        this.Y = iY;
        this.Angle = iAngle;
    }
    return Coordinate;
})();
var PixiRenderer = (function () {
    function PixiRenderer(iElementId) {
        this.UPDATE_RATE = 100;
        this._bunnies = [];
        // Initialize drawing area.
        this._stage = new PIXI.Stage(0x66FF99);
        this._renderer = new PIXI.WebGLRenderer(800, 600);
        document.getElementById(iElementId).appendChild(this._renderer.view);
        this.animate();
    }
    PixiRenderer.prototype.addBunny = function (iId, iTint, iDraggable, iBunnyState, iMoveCallback) {
        // If bunny already exists, update its position.
        for (var i = 0; i < this._bunnies.length; i++) {
            var bunny = this._bunnies[i];
            if (bunny.Id == iId) {
                bunny.Sprite.position.x = iBunnyState.X;
                bunny.Sprite.position.y = iBunnyState.Y;
                bunny.Sprite.rotation = iBunnyState.Angle;
                return;
            }
        }
        // Create the bunny sprite.
        var texture = PIXI.Texture.fromImage("Content/bunny.png");
        var sprite = new PIXI.Sprite(texture);
        sprite.anchor.x = 0.5;
        sprite.anchor.y = 0.5;
        sprite.position.x = iBunnyState.X;
        sprite.position.y = iBunnyState.Y;
        sprite.rotation = iBunnyState.Angle;
        if (iTint == undefined) {
            sprite.tint = Math.random() * 0xFFFFFF;
            iTint = sprite.tint;
        }
        if (iDraggable) {
            sprite.interactive = true;
            sprite.buttonMode = true;
            // Handle the bunny dragging event.
            sprite.mousedown = sprite.touchstart = function (data) {
                this.data = data;
                this.alpha = 0.9;
                this.dragging = true;
            };
            sprite.mouseup = sprite.mouseupoutside = sprite.touchend = sprite.touchendoutside = function (data) {
                this.alpha = 1;
                this.dragging = false;
                this.data = null;
            };
            var updateRate = this.UPDATE_RATE;
            sprite.mousemove = sprite.touchmove = function (data) {
                var _this = this;
                if (this.dragging) {
                    sprite.rotation += 0.1;
                    // need to get parent coords..
                    this._newPosition = this.data.getLocalPosition(this.parent);
                    this.position.x = this._newPosition.x;
                    this.position.y = this._newPosition.y;
                    if (this._interval == undefined)
                        this._interval = setInterval(function () {
                            if (_this._newPosition != null)
                                iMoveCallback(new Coordinate(_this._newPosition.x, _this._newPosition.y, Math.round(sprite.rotation)));
                            _this._newPosition = null;
                        }, updateRate);
                }
            };
        }
        this._stage.addChild(sprite);
        this._bunnies.push({ Id: iId, Sprite: sprite });
    };
    PixiRenderer.prototype.removeBunny = function (iId) {
        for (var i = 0; i < this._bunnies.length; i++) {
            var bunny = this._bunnies[i];
            if (bunny.Id == iId) {
                this._stage.removeChild(bunny.Sprite);
                this._bunnies.splice(i, 1);
                break;
            }
        }
    };
    PixiRenderer.prototype.animate = function () {
        var stage = this._stage;
        var renderer = this._renderer;
        function animate() {
            requestAnimFrame(animate);
            renderer.render(stage);
        }
        requestAnimFrame(animate);
    };
    return PixiRenderer;
})();
var gPixiRenderer = new PixiRenderer("Stage");
