"use strict";
const angular = require("angular");
const codes = require("data/codes.json!json");

module.exports = function () {
    const appName = "bigsbyApp";
    var app = angular.module(appName, ["ngSanitize", "ui.router"]);

    app.controller("homeController", function () {
        this.message = "in control";
    });

    app.component("home", {
        templateUrl: "templates/home.html",
        controller: "homeController"
    });

    app.config(function ($httpProvider, $sceProvider, $stateProvider, $urlRouterProvider) {
        $httpProvider.defaults.useXDomain = true;
        $sceProvider.enabled(false);

        $stateProvider.state({
            "name": "home",
            "url": "/",
            "component": "home"
        });

        if ($urlRouterProvider)
            $urlRouterProvider.otherwise("/");
    });
    
    angular.bootstrap(document, [appName]);
}