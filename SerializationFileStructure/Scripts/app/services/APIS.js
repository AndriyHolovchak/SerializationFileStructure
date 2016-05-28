angular.module('SerializationFileStructure')
    .factory('API', ['$http', api])

function api($http) {
    function serialiseToFile(body) {
        return $http.post("/api/serialize/", body);
    }

    return {
        serialiseToFile
    }
}