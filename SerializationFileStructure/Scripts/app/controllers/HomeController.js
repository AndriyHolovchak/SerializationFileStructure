angular.module("SerializationFileStructure")
    .controller("HomeController",
    [
        "$scope", "API", "Upload", HomeController
    ]);

function HomeController($scope, API, Upload) {

    function serializeToFile(data) {
        return API.serialiseToFile(data).then(function (res) {
            console.log(res);
        });
    }

    function deserialiseToFile(data) {
        return API.deserialiseToFile(data).then(function (res) {
            console.log(res);
        });
    }

    $scope.serialize = function (data) {
        console.log(data);

        serializeToFile(data);
    }

    //$scope.uploadme

   /* $scope.deserialize = function (data) {
        console.log(data);
        console.log($scope.uploadme);

        var body = {
            "DeserializePath": data.deserializePath,
            "File": $scope.uploadme
        }

        deserialiseToFile(body);
    }*/

    $scope.deserialize = function (file, data) {


        var sendObj = {        
                file: file,
                path: data.deserializePath
        }
        Upload.upload({
            method: 'POST',
            url: "/api/deserialize/",
            data: sendObj
        });

    }
}