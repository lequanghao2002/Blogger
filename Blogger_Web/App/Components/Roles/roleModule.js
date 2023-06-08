/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function() {
    angular.module('BloggerRole', ['BloggerCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('role_list', {
                url: '/role_list',
                templateUrl: '/App/Components/Roles/roleListView.html',
                controller: 'roleListController'
            })
            .state('role_add', {
                url: '/role_add',
                templateUrl: '/App/Components/Roles/roleAddView.html',
                controller: 'roleAddController'
            })
            .state('role_edit', {
                url: '/role_edit/:id',
                templateUrl: '/App/Components/Roles/roleEditView.html',
                controller: 'roleEditController'
            })

    }
})();