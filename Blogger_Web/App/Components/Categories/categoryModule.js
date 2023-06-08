/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function () {
    angular.module('BloggerCategory', ['BloggerCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('category_list', {
                url: "/category_list",
                templateUrl: '/app/Components/Categories/categoryListView.html',
                controller: 'categoryListController'
            })
            .state('category_add', {
                url: '/category_add',
                templateUrl: '/app/components/categories/categoryAddView.html',
                controller: 'categoryAddController'
            })
            .state('category_edit', {
                url: '/category_edit/:id',
                templateUrl: '/app/components/categories/categoryEditView.html',
                controller: 'categoryEditController'
            })
    };


})();