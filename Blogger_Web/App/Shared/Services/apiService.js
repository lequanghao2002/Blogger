/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http'];

    function apiService($http) {
        return {
            get: get
        }

        function get(url, params, success, failed) {
            $http.get(url, params).then((result) => {
                success(result);
            }, (error) => {
                failed(error);
            });
        }

    }
})(angular.module('BloggerCommon'));