requirejs.config({
    "baseUrl": "scripts",
    paths: {
        "angular": "lib/angular.min",
        "jquery": "lib/jquery.min",
        "components": "components"
    },
    urlArgs: "bust=" + (new Date()).getTime()
});

requirejs(["angular"], function () {
    angular.module("theApp", []);
    
    requirejs(["components"], function (components) {
        angular.forEach(components, function (component) {
            requirejs(["./services/" + component.id + "Service", "./controllers/" + component.id + "Controller"], function (shellService, shellController) {
                angular.bootstrap(document, ["theApp"]);
            });
        });
        
    });
});