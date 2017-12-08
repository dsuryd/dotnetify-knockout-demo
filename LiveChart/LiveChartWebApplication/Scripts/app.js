window.jQuery = require("jquery");
var dotnetify = require("dotnetify/dist/dotnetify");
var signalR = require("dotnetify/dist/signalR-netfx");
dotnetify.hubLib = signalR;