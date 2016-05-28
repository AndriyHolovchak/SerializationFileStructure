angular.module("SerializationFileStructure")
    .controller("HomeController",
    [
        "$scope", "API", HomeController
    ]);

function HomeController($scope, API) {

    function serializeToFile(data) {
        return API.serialiseToFile(data).then(function (res) {
            console.log(res);
        });
    }

    $scope.serialize = function (data) {
        console.log(data);

        serializeToFile(data);
    }
}