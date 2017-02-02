(function () {
    "use strict";

    var appName = "theApp";

    var app = angular.module(appName, ["ngSanitize","ui.router"]);

    app.component("home", {
        templateUrl: "templates/home.html"
    });
    app.component("one", {
        templateUrl: "templates/pageOne.html"
    });
    app.component("two", {
        templateUrl: "templates/pageTwo.html"
    });

    app.config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state({
            name: "home",
            url:"/",
            component:"home"
        });

        $stateProvider.state({
            name: "one",
            url: "/one",
            component: "one"
        });

        $stateProvider.state({
            name: "two",
            url: "/two",
            component: "two"
        });

        $urlRouterProvider.otherwise("/");
    });
    angular.bootstrap(document, [appName]);
})();