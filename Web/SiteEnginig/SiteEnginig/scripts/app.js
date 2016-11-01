var appName = "mbApp";

(function () {
    "user strict";

    var apiUrl = "../data/";

    requirejs.config({
        paths: {
            "angular": "lib/angular.min",
            "angularRoute": "lib/angular_1_router",
            "text": "lib/require-text",
            "json": "lib/require-json",
            "api": apiUrl
        }
    });

    requirejs(["json!api/shell.json", "angular", "angularRoute"], function (shell) {
        var module = angular.module(appName, ["ngComponentRouter"]);
        module.value("$routerRootComponent", "mbApp");

        module.component("mbApp", {
            templateUrl: "/templates/app.html",
            $routeConig: [
                { path: "/home", component: "home", "name": "Home" },

            ]
        });
    });
})();