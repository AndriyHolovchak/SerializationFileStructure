angular.module('SerializationFileStructure')
    .factory('API', ['$http', api])

function api($http) {
    function serialiseToFile(body) {
        return $http.post("/api/serialize/", body);
    }

    function deserialiseToFile(body) {
        return $http.post("/api/deserialize/", body);
    }

    return {
        serialiseToFile,
        deserialiseToFile
    }
}