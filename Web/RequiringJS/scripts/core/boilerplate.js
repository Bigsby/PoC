"use strict";
requirejs.config({
    baseUrl: "/scripts",
    paths: {
        "text": "https://cdnjs.cloudflare.com/ajax/libs/require-text/2.0.12/text.min",
        "json": "https://cdnjs.cloudflare.com/ajax/libs/requirejs-plugins/1.0.3/json.min",
        "coreLibraries": "core/coreLibraries.json",
        "appLibraries": "appLibraries.json"
    },
});

requirejs(["json!coreLibraries", "json!appLibraries", "appBuilder"], function (coreLibraries, appLibraries, appBuilder) {

    var head = document.getElementsByTagName("head")[0];
    appLibraries.styles.forEach(function (fileName) {
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", fileName);
        head.appendChild(fileref);
    });

    requirejs.config(coreLibraries.config);
    requirejs.config(appLibraries.config);

    requirejs(coreLibraries.requires.concat(appLibraries.requires), function () {
        var app = angular.module(coreLibraries.angularAppName, coreLibraries.angularModules.concat(appLibraries.angularModules));

        if (appLibraries.ga)
            app.run(function ($window, $transitions, $location) {
                if (libraries.ga) {
                    $window.ga("create", libraries.ga, "auto");
                    $transitions.onSuccess({}, () => {
                        $window.ga("send", "pageview", $location.path());
                    });
                }
            });

        window.template = function(name){
            return "templates/" + name + ".html";
        };
        
        appBuilder.RegisterComponents(app);

        app.config(["$httpProvider", "$sceProvider", "$stateProvider", "$urlRouterProvider", "$locationProvider",
            function ($httpProvider, $sceProvider, $stateProvider, $urlRouterProvider, $locationProvider) {
                $httpProvider.defaults.useXDomain = true;
                $sceProvider.enabled(false);

                appBuilder.RegisterStates($stateProvider);

                $urlRouterProvider.otherwise("/");
            }
        ]);

        angular.bootstrap(document, [coreLibraries.angularAppName]);
    });
});