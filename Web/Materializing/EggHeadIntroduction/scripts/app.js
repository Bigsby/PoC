(function () {
    "use strict";
    var appName = "testApp";
    var mainComponentName = "appContainer";

    requirejs.config({
        "paths": {
            "text": "lib/require-text",
            "json": "lib/require-json"
        },
        onerror: function (error) {
            alert("error");
        }
    });

    function loadCss(style) {
        var link = document.createElement("link");
        link.type = "text/css";
        link.rel = "stylesheet";
        link.href = "styles/" + style.path;
        document.getElementsByTagName("head")[0].appendChild(link);
        console.log("Loade: " + link.href);
    }

    requirejs(["json!../shell.json"], function (shell) {
        var sortedStyles = shell.styles.sort(function (a, b) { return a.order - b.order; });

        for (var styleIndex = 0; styleIndex < sortedStyles.length; styleIndex++) {
            loadCss(sortedStyles[styleIndex]);
        }

        requirejs([shell.scripts.path], function () {
            console.log("Loaded: " + shell.scripts.path);
            requirejs(shell.scripts.dependants, function () {
                console.log("Loaded: " + shell.scripts.dependants.join());
                var module = angular.module(appName, ["ngComponentRouter", "ngSanitize", "ngMaterial"]);
                module.component(mainComponentName, {
                    templateUrl: "templates/app.html",
                    $routeConfig: [
                        {
                            path: "/first",
                            component: "first",
                            name: "First"
                        },
                        {
                            path: "/second",
                            component: "second",
                            name: "Second"
                        },
                        {
                            path: "/third",
                            component: "third",
                            name: "Third"
                        },
                        {
                            path: "/fourth",
                            component: "fourth",
                            name: "Fourth"
                        }
                    ]
                });

                module.component("first", {
                    templateUrl: "templates/first.html"
                });

                module.component("second", {
                    templateUrl: "templates/second.html"
                });

                module.component("third", {
                    templateUrl: "templates/third.html"
                });

                module.component("fourth", {
                    templateUrl: "templates/fourth.html"
                });

                module.value("$routerRootComponent", mainComponentName);

                angular.bootstrap(document, [appName]);
            });
        });
    });
})();