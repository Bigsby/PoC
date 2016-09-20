angular.module("theApp").controller("shellController", ["shellService", function (shellService)
{
    this.text = "Service says:" + shellService.getText();
    this.Loaded = true;
}]);