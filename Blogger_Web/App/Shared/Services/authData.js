(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        // chứa thông tin user khi đăng nhập

        return authDataFactory;
    }]);
})(angular.module('BloggerCommon'));