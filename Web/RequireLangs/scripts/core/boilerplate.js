"use strict";
requirejs.config({
    baseUrl: "/scripts",
    paths: {
        "text": "https://cdnjs.cloudflare.com/ajax/libs/require-text/2.0.12/text.min",
        "json": "https://cdnjs.cloudflare.com/ajax/libs/requirejs-plugins/1.0.3/json.min",
        "coreConfig": "core/config.json",
        "appConfig": "app/config.json",
        "data": "./../data/"
    },
});

requirejs(["json!coreConfig", "json!appConfig", "app/appBuilder"], function (coreConfig, appConfig, appBuilder) {

    var head = document.getElementsByTagName("head")[0];
    appConfig.styles.forEach(function (fileName) {
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", fileName + ".css");
        head.appendChild(fileref);
    });

    requirejs.config(coreConfig.config);
    requirejs.config(appConfig.config);

    var dataRequires = [];
    var dataRequired = false;
    if (appConfig.initialData) {
        dataRequired = true;
        appConfig.initialData.forEach(function (file) {
            dataRequires.push("json!data/" + file + ".json");
        });
    }

    requirejs(dataRequires.concat(coreConfig.requires.concat(appConfig.requires)), function () {
        var data = {};
        if (dataRequired) {
            for (var dataIndex = 0; dataIndex < appConfig.initialData.length; dataIndex++) {
                data[appConfig.initialData[dataIndex]] = arguments[dataIndex];
            }
        }

        appBuilder.ProcessData(data);

        var app = angular.module(coreConfig.angularAppName, coreConfig.angularModules.concat(appConfig.angularModules));
        app.value("data", data);

        if (appConfig.ga)
            app.run(function ($window, $transitions, $location) {
                if ($window.ga) {
                    $window.ga("create", appConfig.ga, "auto");
                    $transitions.onSuccess({}, () => {
                        $window.ga("send", "pageview", $location.path());
                    });
                }
            });

        window.templatePath = function (name) {
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

        angular.bootstrap(document, [coreConfig.angularAppName]);
    });
});