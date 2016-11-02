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

            shell.sections.push({
                id: "home",
                name: "Home"
            });

            shell.sections.push({
                id: "appHeader",
                name: "Header",
                skipRoute: true
            });

            shell.sections.push({
                id: "appFooter",
                name: "Footer",
                skipRoute: true
            });

            angular.forEach(shell.sections, function (section) {

                app.component(section.id, {
                    templateUrl: "data/templates/" + section.id + ".html",
                    controller: function () {
                        var vm = this;
                        vm.message = "This is " + section.name + " message";
                        vm.$routerOnActivate = function (next, previous) {
                            if (next.params.id)
                                vm.message = "Your id is: " + next.params.id;
                        };
                    }
                });

                if (!section.skipRoute) {
                    routes.push({
                        path: "/" + (section.id == "home" ? "" : section.id),
                        component: section.id,
                        name: section.name
                    });
                    if (section.id != "home")
                        routes.push({
                            path: "/" + section.id + "/:id",
                            component: section.id,
                            name: section.name + "Details"
                        });
                }

            });

            routes.push({
                path: "/**",
                redirectTo: ["Home", ""]
            });

            app.component("appContainer", {
                templateUrl: "data/templates/shell.html",
                $routeConfig: routes
            });

            angular.bootstrap(document, [appName]);
        });
    });
})();