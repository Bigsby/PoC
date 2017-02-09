(function () {
    "use strict";

    var appName = "ngCode";

    var app = angular.module(appName, ["ngSanitize", "ui.router"]);

    app.run(function ($http) {
        function loadjscssfile(filename, filetype) {
            if (filetype == "js") { //if filename is a external JavaScript file
                var fileref = document.createElement('script')
                fileref.setAttribute("type", "text/javascript")
                fileref.setAttribute("src", filename)
            }
            else if (filetype == "css") { //if filename is an external CSS file
                var fileref = document.createElement("link")
                fileref.setAttribute("rel", "stylesheet")
                fileref.setAttribute("type", "text/css")
                fileref.setAttribute("href", filename)
            }
            if (typeof fileref != "undefined")
                document.getElementsByTagName("head")[0].appendChild(fileref)
        }

        $http.get("data/all.json").then(function (response) {
            var codeMirror = response.data.codeMirror;

            codeMirror.css.forEach(function (css) {
                loadjscssfile(codeMirror.baseUrl + css + ".css", "css");
            });

            codeMirror.script.forEach(function (js) {
                loadjscssfile(codeMirror.baseUrl + js + ".js", "js");
            });
        });
    });

    app.directive("codeHighlight", function ($http) {
        return {
            restrict: "E",
            link: function ($scope, element, attrs) {
                $http.get("https://raw.githubusercontent.com/Bigsby/HelloLanguages/master/" + attrs.src)
                .then(function (response) {
                    CodeMirror(element[0], {
                        mode: attrs.language,
                        value: response.data,
                        theme: "ambiance",
                        readOnly: "nocursor"
                    });
                });
            }
        }
    });

    app.component("codes", {
        templateUrl: "templates/codes.html",
        controller: function ($http) {
            var vm = this;
            $http.get("data/all.json").then(function (response) {
                angular.extend(vm, response.data);
            });
        }
    });

    angular.bootstrap(document, [appName]);
})();