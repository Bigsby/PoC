angular.module("theApp").service("shellService", function () {
    this.getText = function () {
        return "This is from the service";
    };
});