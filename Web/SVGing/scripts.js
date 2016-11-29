var app = angular.module("app", []);

app.controller("svgController", function(){
    var me = this;
    var interval = 2000;

    function change(){
        me.value = me.value == 10 ? 50 : 10;
        setTimeout(change, interval);
    }

    setTimeout(change, interval);
});

angular.bootstrap(document, ["app"]);