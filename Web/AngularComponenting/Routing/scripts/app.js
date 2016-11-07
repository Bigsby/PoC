var moduleName = "app";
var templatesPath = "templates/";

var data = {
    scopes: [
        {
            id: "scope1",
            name: "Scope 1",
            collections: [
                {
                    id: "collection11",
                    name: "Collection 1.2",
                    items: [
                        {
                            id: "item111",
                            name: "Item 1.1.1"
                        },
                        {
                            id: "item112",
                            name: "Item 1.1.2"
                        }
                    ]
                },
                {
                    id: "collection12",
                    name: "Collection 1.2",
                    items: [
                        {
                            id: "item121",
                            name: "Item 1.2.1"
                        },
                        {
                            id: "item112",
                            name: "Item 1.2.2"
                        }
                    ]
                }
            ]
        },
        {
            id: "scope2",
            name: "Scope 2",
            collections: [
                {
                    id: "collection21",
                    name: "Collection 2.2",
                    items: [
                        {
                            id: "item211",
                            name: "Item 2.1.1"
                        },
                        {
                            id: "item212",
                            name: "Item 2.1.2"
                        }
                    ]
                },
                {
                    id: "collection22",
                    name: "Collection 2.2",
                    items: [
                        {
                            id: "item221",
                            name: "Item 2.2.1"
                        },
                        {
                            id: "item112",
                            name: "Item 2.2.2"
                        }
                    ]
                }
            ]
        }
    ]
};

var module = angular.module(moduleName, ["ngComponentRouter"]);
module.value("$routerRootComponent", "mbApp");

module.component("simple", {
    templateUrl: templatesPath + "simple.html"
});

module.component("level1Home", {
    templateUrl: templatesPath + "level1Home.html"
});

module.component("level11", {
    templateUrl: templatesPath + "level11.html"
});

module.component("level12", {
    templateUrl: templatesPath + "level12.html"
});

module.component("level1", {
    templateUrl: templatesPath + "level1.html",
    $routeConfig: [
        { path: "/", component: "level1Home", name: "Level1Home" },
        { path: "/one", component: "level11", name: "Level1" },
        { path: "/two", component: "level12", name: "Level2" }
    ]
});

module.component("paraSimple", {
    templateUrl: templatesPath + "paramSimple.html",
    controller: function () {
        var vm = this;
        vm.$routerOnActivate = function (next, previous) {
            angular.extend(vm, next.params);
        }
    }
});

module.component("itemDetail", {
    templateUrl: templatesPath + "itemDetail.html",
    controller: function () {
        var vm = this;
        vm.$routerOnActivate = function (next, previous) {
            angular.extend(vm, next.params);
        }
    }
});

module.component("deepHome", {
    templateUrl: templatesPath + "deepHome.html"
});

module.component("paraDeep", {
    templateUrl: templatesPath + "paramDeep.html",
    $routeConfig: [
        { path: "/", component: "deepHome", name: "Home" },
        { path: "/:id", component: "itemDetail", name: "Detail" }
    ]
});

module.component("scopes", {
    template: "Here<ng-outlet></ng-outlet>",
    $routeConfig: [
        { path: "/", component: "scopesList", name: "Home" },
        { path: "/:scope", component: "idsCollections", name: "Collections" },
        { path: "/:scope/:collection", component: "idsItems", name: "Items" },
        { path: "/:scope/:collection/:item", component: "idsItemDetails", name: "ItemDetails" }
    ]
});

module.component("scopesList", {
    templateUrl: templatesPath + "idsScopes.html",
    controller: function () {
        this.scopes = data.scopes;
    }
});

module.component("idsCollections", {
    templateUrl: templatesPath + "idsCollections.html",
    controller: function () {
        var vm = this;
        vm.$routerOnActivate = function (next, previous) {
            console.log("In collectsions");
            vm.scope = next.params.scope;

            var scope;
            for (var scopeIndex = 0; scopeIndex < data.scopes.length; scopeIndex++) {
                if (data.scopes[scopeIndex].id === vm.scope) {
                    angular.extend(vm, data.scopes[scopeIndex]);
                    break;
                }
            }
        }
    }
});

module.component("idsItems", {
    templateUrl: templatesPath + "idtsItems.html",
    controller: function () {
        var vm = this;
        vm.$routerOnActivate = function (next, previous) {
            console.log("In idsItems");
            vm.collection = next.params.collection;
            vm.scope = next.params.scope;

            var scope;
            for (var scopeIndex = 0; scopeIndex < data.scopes.length; scopeIndex++) {
                if (data.scopes[scopeIndex].id === vm.scope) {
                    scope = data.scopes[scopeIndex];
                    break;
                }
            }

            if (scope) {
                for (var collectionIndex = 0; collectionIndex < scope.collections.length; collectionIndex++) {
                    if (scope.collections[collectionIndex].id === vm.collection) {
                        angular.extend(vm, scope.collections[collectionIndex]);
                        break;
                    }
                }
            }
        }
    }
});

module.component("idsItemDetails", {
    templateUrl: templatesPath + "itemDetail.html",
    controller: function () {
        console.log("in item details");
        var vm = this;
        vm.$routerOnActivate = function (next, previous) {
            vm.id = next.params.item;
            vm.collection = next.params.collection;
            vm.scope = next.params.scope;

            var scope;
            for (var scopeIndex = 0; scopeIndex < data.scopes.length; scopeIndex++) {
                if (data.scopes[scopeIndex].id === vm.scope) {
                    scope = data.scopes[scopeIndex];
                    break;
                }
            }

            var collection;
            if (scope) {
                for (var collectionIndex = 0; collectionIndex < scope.collections.length; collectionIndex++) {
                    if (scope.collections[collectionIndex].id === vm.collection) {
                        collection = scope.collections[collectionIndex];
                        break;
                    }
                }

                if (collection) {
                    for (var itemIndex = 0; itemIndex < collection.items.length; itemIndex++) {
                        if (collection.items[itemIndex].id === vm.id) {
                            angular.extend(vm, collection.items[itemIndex]);
                            break;
                        }
                    }
                }
            }
        }
    }
});

module.component("mbApp", {
    templateUrl: templatesPath + "mbApp.html",
    $routeConfig: [
        { path: "/", component: "simple", name: "Simple" },
        { path: "/l/...", component: "level1", name: "Level1" },
        { path: "/p", component: "paraSimple", name: "ParaSimple" },
        { path: "/p/:id", component: "paraSimple", name: "ParaRoute" },
        { path: "/d/...", component: "paraDeep", name: "ParaDeep" },
        { path: "/s/...", component: "scopes", name: "Scopes" }
    ]
});


angular.bootstrap(document, [moduleName]);