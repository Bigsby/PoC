angular = require("angular");
var comps = require("http://localhost:52301/data/components.json");

angular.forEach(comps, function (component) {
    alert("component found " + component.id);
});