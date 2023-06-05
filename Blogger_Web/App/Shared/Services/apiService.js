/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.service('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            delete: del
        }

        function get(url, params, success, failed) {
            $http.get(url, params).then((result) => {
                success(result);
            }, (error) => {
                failed(error);
            });
        }

        function post(url, data, success, failed) {
            $http.post(url, data).then((result) => {
                success(result);
            }, (error) => {
                if (error == '401') {
                    notificationService.displayError('authentication failure');
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }

        function put(url, data, success, failed) {
            $http.put(url, data).then((result) => {
                success(result);
            }, (error) => {
                if (error == '401') {
                    notificationService.displayError('authentication failure');
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }

        function del(url, data, success, failed) {
            $http.delete(url, data).then((result) => {
                success(result);
            }, (error) => {
                if (error == '401') {
                    notificationService.displayError('authentication failure');
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }

    }
})(angular.module('BloggerCommon'));