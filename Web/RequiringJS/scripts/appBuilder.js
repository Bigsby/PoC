"use strict";
(function () {
    // var here = document.createElement("span");
    // here.textContent = "app created!";
    // document.body.appendChild(here);

    // var theCode = document.getElementById("theCode");
    // Prism.highlightElement(theCode);

    function RegisterComponents(app) {
        app.component("home", {
            templateUrl: template("home")
        });
    }

    function RegisterStates(stateProvider) {
        stateProvider.state({
            name: "home",
            url: "/",
            component: "home"
        });
    }

    
    define([], function () {
        return {
            RegisterComponents: RegisterComponents,
            RegisterStates: RegisterStates
        };
    });
})();