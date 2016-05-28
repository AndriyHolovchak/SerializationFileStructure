angular.module("SerializationFileStructure")
    .controller("HomeController",
    [
        "$scope", HomeController
    ]);

function HomeController($scope) {
    $scope.test = "TEST TEST";
}