angular.module("SerializationFileStructure", ["ngRoute", "ngFileUpload"])
    .config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when("/",
        {
            templateUrl: "/scripts/app/templates/index.html",
            controller: "HomeController"
        });
    }]);