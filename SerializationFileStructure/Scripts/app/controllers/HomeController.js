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
            $scope.serializeSuccess = res.data;
            $scope.disabled = false;
        }, function (err) {
            $scope.serData = {};
            $scope.isSerializing = false;
            $scope.serializeError = err.data;
            $scope.disabled = false;
        });
    }

    $scope.serialize = function (serData) {
        $scope.disabled = true;
        $scope.isSerializing = true;
        serializeToFile(serData);
    }

    $scope.resetSerialize = function() {
        $scope.serData = {};
        $scope.serializeSuccess = null;
        $scope.serializeError = null;
    }

    $scope.resetDeserialize = function () {
        $scope.deserData = {};
        $scope.file = null;
        $scope.error = null;
        $scope.updateError = null;
        $scope.success = null;
    }

    $scope.deserialize = function (file, data) {
        if (!file) {
            $scope.error = "File is required!";
            return;
        }
        $scope.disabled = true;
        $scope.isDeserializing = true;
        var sendObj = {        
                file: file,
                path: data.deserializePath
        }
        Upload.upload({
            method: 'POST',
            url: "/api/deserialize/",
            data: sendObj
        }).then(function (res) {
            console.log(res);
            $scope.deserData = {};
            $scope.file = null;
            $scope.isDeserializing = false;
            $scope.success = res.data;
            $scope.updateError = null;
            $scope.disabled = false;
        }, function (err) {
            if (err.status == 400) {
                $scope.updateError = err.data;
            } else {
                $scope.updateError = err.statusText +". "+ err.data.Message;
            }
            $scope.disabled = false;
            $scope.isDeserializing = false;
        });

    }
}