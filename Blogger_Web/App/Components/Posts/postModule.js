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
            .state('post_add', {
                url: '/post_add',
                templateUrl: '/App/Components/Posts/postAddView.html',
                controller: 'postAddController'
            })
            .state('post_edit', {
                url: '/post_edit/:id',
                templateUrl: '/App/Components/Posts/postEditView.html',
                controller: 'postEditController'
            })
            .state('post_detail', {
                url: '/post_detail/:id',
                templateUrl: '/App/Components/Posts/postDetailView.html',
                controller: 'postDetailController'
            })
    }
})();