SystemJS.config({
    "map": {
        "json": "https://cdnjs.cloudflare.com/ajax/libs/systemjs-plugin-json/0.3.0/json.min.js",
        "app": "scripts/app.js",
        "angular": "https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular.min.js",
        "angular-sanitize": "https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.1/angular-sanitize.min.js",
            "angular-ui-router": "https://cdnjs.cloudflare.com/ajax/libs/angular-ui-router/1.0.0-rc.1/angular-ui-router.min.js"
    },
    "meta":{
        "angular-sanitize":{
            "deps":[
                "angular"
            ]
        },
        "angular-ui-router":{
            "deps":[
                "angular"
            ]
        },
        "app":{
            "deps":[
                "angular-sanitize",
                "angular-ui-router"
            ]
        }
    }
});
SystemJS.import("app").then(function (app) { app(); });