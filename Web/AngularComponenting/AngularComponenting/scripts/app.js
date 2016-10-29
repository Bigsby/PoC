(function () {
    "use strict";

    requirejs.config({
        paths: {
            "angular": "https://ajax.googleapis.com/ajax/libs/angularjs/1.5.8/angular.min"
        }
    });
    requirejs(["angular"], function() {
        var app = angular.module("theApp", ["ngComponentRouter"]);
        app.value("$routerRootComponent", "appContainer");
        app.component("")
        app.component("appContainer", {
            templateUrl: "shell.html",
            $routeConfig: [
                {
                    path: "/one",
                    component: "pageOne",
                    name: "One"
                },
                {
                    path: "/two",
                    component: "pageTwo",
                    name: "Two"
                }
            ]
        });
    });
})();