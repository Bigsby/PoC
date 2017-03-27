"use strict";
(function () {
    define(["app/processData", "app/states", "app/components"], function (ProcessData, RegisterStates, RegisterComponents) {
        return {
            RegisterComponents: RegisterComponents,
            RegisterStates: RegisterStates,
            ProcessData: ProcessData
        };
    });
})();