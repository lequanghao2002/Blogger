/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function () {
    angular.module('BloggerPost', ['BloggerCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('post_list', {
                url: '/post_list',
                templateUrl: '/App/Components/Posts/postListView.html',
                controller: 'postListController'
            })
    }
})();