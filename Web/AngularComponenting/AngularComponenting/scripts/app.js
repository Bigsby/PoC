var appName = "theApp";

(function () {
    "use strict";
    var apiUrl = "../data";
    requirejs.config({
        paths: {
            "angular": "lib/angular.min",
            "angularRouter": "lib/angular_1_router",
            "text": "lib/require-text",
            "json": "lib/require-json",
            "api": apiUrl
        }
    });

    requirejs(["angular"], function () {

        requirejs(["json!api/structure/shell.json", "angularRouter"], function (shell) {
            
            var app = angular.module(appName, ["ngComponentRouter"]);
            app.value("$routerRootComponent", "appContainer");
            
            var routes = [];

            angular.forEach(shell.sections, function (section) {
                app.component(section.id, {
                    templateUrl: "data/templates/" + section.id + ".html",
                    controller: function () {
                        this.message = "This is " + section.name + " message"
                    }
                });

                routes.push({
                    path: "/" + section.id,
                    component: section.id,
                    name: section.name
                });
            });

            app.component("appContainer", {
                templateUrl: "data/templates/shell.html",
                $routeConfig: routes
            });

            angular.bootstrap(document, [appName]);
        });
    });
})();