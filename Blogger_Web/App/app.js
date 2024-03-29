﻿/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function () {
    angular.module('Blogger',
        [
            'BloggerCommon',
            'BloggerCategory',
            'BloggerPost',
            'BloggerRole',
        ])
        .config(config)
        .config(configAuthentication);

    // Hai inject này nằm trong ui-router
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            })
            .state('login', {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'loginController'
            })
            .state('home', {
                // Đường dẫn vào view
                url: '/admin',
                parent: 'base',
                // Vào view này nó tự nhận và sử dụng ctrl ở dưới
                templateUrl: '/app/components/home/homeView.html',
                // Không khai báo homeController thì bị lỗi
                controller: 'homeController'
            });

        $urlRouterProvider.otherwise('/login');
    };

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();