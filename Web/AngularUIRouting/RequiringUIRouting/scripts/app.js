define(["appLoad", "uiRouter"], function (uiRouter) {
    var appName = "theApp";

    console.log("Starting app");
    angular.module(appName, ["ui.router"]);

    angular.bootstrap(document, [appName]);
});