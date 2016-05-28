angular.module("SerializationFileStructure")
    .controller("HomeController",
    [
        "$scope", "API", "Upload", HomeController
    ]);

function HomeController($scope, API, Upload) {
    $scope.serData = {};
    $scope.deserData = {};
    $scope.isDeserializing = false;
    $scope.isSerializing = false;

    function serializeToFile(data) {
        return API.serialiseToFile(data).then(function (res) {
            $scope.serData = {};
            $scope.isSerializing = false;
        });
    }

    $scope.serialize = function (serData) {
        $scope.isSerializing = true;
        serializeToFile(serData);
    }

    $scope.resetSerialize = function() {
        $scope.serData = {};
    }

    $scope.resetDeserialize = function () {
        $scope.deserData = {};
        $scope.file = null;
        $scope.error = null;
        $scope.updateError = null;
    }

    $scope.deserialize = function (file, data) {
        if (!file) {
            $scope.error = "File is required!";
            return;
        }
        $scope.isDeserializing = true;
        var sendObj = {        
                file: file,
                path: data.deserializePath
        }
        Upload.upload({
            method: 'POST',
            url: "/api/deserialize/",
            data: sendObj
        }).then(function(res) {
            $scope.deserData = {};
            $scope.file = null;
            $scope.isDeserializing = false;
        }, function (error) {
            $scope.updateError = "Something happened with your updating!";
        });

    }
}