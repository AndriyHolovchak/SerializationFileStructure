angular.module("SerializationFileStructure", ["ngRoute"])
    .config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when("/",
        {
            templateUrl: "/scripts/app/templates/index.html",
            controller: "HomeController"
        });
    }]);