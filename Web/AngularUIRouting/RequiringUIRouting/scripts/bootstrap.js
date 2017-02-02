requirejs.config({
    waitSeconds: 5,
    paths: {
        'angular': 'lib/angular.min',
        "uiRouter": "lib/angular-ui-router.min"
    },
    shim: {
        'angular': {
            exports: 'angular'
        },
        'uiRouter': {
            deps: ['angular']
        }
    },

    deps: [
        './app'
    ]
});