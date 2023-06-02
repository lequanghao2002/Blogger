/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function () {
    angular.module('Blogger',
        [
            'BloggerCategory',
            'BloggerCommon'
        ]).config(config);

    // Hai inject này nằm trong ui-router
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('home', {
                // Đường dẫn vào view
                url: '/admin',
                // Vào view này nó tự nhận và sử dụng ctrl ở dưới
                templateUrl: '/app/components/home/homeView.html',
                // Không khai báo homeController thì bị lỗi
                controller: 'homeController'
            });

        $urlRouterProvider.otherwise('/admin');
    };
})();