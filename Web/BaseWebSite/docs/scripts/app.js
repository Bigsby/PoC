"use strict";
requirejs.config({
    paths: {
        text: "libs/text",
        json: "libs/json",
        data: "./../data/",
        cdn: "https://cdnjs.cloudflare.com/ajax/libs"
    }
});

var Helpers = new (function helpers() {
    var templateRoot = "templates/";
    this.templatePath = function (name) {
        return templateRoot + name + ".html";
    }
});


requirejs(["json!data/libraries.json"], function (libraries) {
    requirejs(libraries.firstRequires, function () {
        requirejs(libraries.secondRequires, function () {
            var appName = "theApp";
            var templateRoot = "templates/";
            var app = angular.module(appName, libraries.angularModules);

            app.component("home", {
                templateUrl: Helpers.templatePath("home")
            });

            app.component("controls", {
                templateUrl: Helpers.templatePath("controls")
            });

            app.config(["$httpProvider", "$sceProvider", "$stateProvider", "$urlRouterProvider", "$locationProvider",
                function ($httpProvider, $sceProvider, $stateProvider, $urlRouterProvider, $locationProvider) {
                    $httpProvider.defaults.useXDomain = true;
                    $sceProvider.enabled(false);

                    $stateProvider.state({
                        name: "home",
                        url: "/",
                        component: "home"
                    });

                    $stateProvider.state({
                        name: "controls",
                        url: "/controls",
                        component: "controls"
                    });

                    $urlRouterProvider.otherwise("/");
                }
            ]);

            angular.bootstrap(document, [appName]);
        });
    });
});