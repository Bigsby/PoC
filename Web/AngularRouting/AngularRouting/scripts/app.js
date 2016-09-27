'use strict';

requirejs([], function () {
    var app = angular.module("theApp", ["ngRoute"]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "pages/main.html"
            })
            .when("/one/:id?", {
                templateUrl: "pages/pageOne.html"
            })
            .when("/two", {
                templateUrl: "pages/pageTwo.html"
            })
            .otherwise({
                redirectTo: '/'
            });
    });

    app.controller("oneController", ["$routeParams", function ($routeParams) {
        this.id = $routeParams.id;
    }]);

    angular.bootstrap(document, ["theApp"]);
});