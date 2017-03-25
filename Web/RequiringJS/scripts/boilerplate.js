requirejs.config({
    paths: {
        "text": "https://cdnjs.cloudflare.com/ajax/libs/require-text/2.0.12/text.min",
        "json": "https://cdnjs.cloudflare.com/ajax/libs/requirejs-plugins/1.0.3/json.min",
        "coreLibraries": "coreLibraries.json",
        "appLibraries": "appLibraries.json"
    },
});

requirejs(["json!coreLibraries", "json!appLibraries"], function (coreLibraries, appLibraries) {
    
    var head = document.getElementsByTagName("head")[0];
    coreLibraries.styles.concat(appLibraries.styles).forEach(function (fileName) {
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", fileName);
        head.appendChild(fileref);
    });

    requirejs.config(coreLibraries.config);
    requirejs.config(appLibraries.config);
    
    requirejs(coreLibraries.requires.concat(appLibraries.requires), function () {
        var app = angular.module("app", ["ngSanitize", "ui.router"]);
        var here = document.createElement("span");
        here.textContent = "app created!";
        document.body.appendChild(here);

        var theCode = document.getElementById("theCode");
        Prism.highlightElement(theCode);
    });
});