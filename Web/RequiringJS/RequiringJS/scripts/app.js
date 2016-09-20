requirejs.config({
    paths: {
        "angular": "lib/angular.min",
        "jquery": "lib/jquery.min",
        "text": "lib/require-text",
        "json": "lib/require-json",
        "components": "../data/components.json"
    },
    urlArgs: "bust=" + (new Date()).getTime()
});

requirejs(["angular"], function () {
    angular.module("theApp", []);
    
    requirejs(["json!components"], function (components) {
        var componentsToLoad = [];
        angular.forEach(components, function (component) {
            if (component.hasService)
                componentsToLoad.push("services/" + component.id + "Service");

            if (component.hasController)
                componentsToLoad.push("controllers/" + component.id + "Controller");
        });

        requirejs(componentsToLoad, function () {
            angular.bootstrap(document, ["theApp"]);
        });
    });
});