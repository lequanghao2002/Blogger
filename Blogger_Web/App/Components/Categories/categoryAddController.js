/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('categoryAddController', categoryAddController);

    categoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']

    function categoryAddController($scope, apiService, notificationService, $state) {
        $scope.category = {};
        
        $scope.addCategory = () => {
            apiService.post('/api/Categories/create-category', $scope.category, (result) => {
                notificationService.displaySuccess('Create category successfully');
                $state.go('category_list');
            }, (error) => {
                notificationService.displayError('Create category failed');
            });
        };
    };

})(angular.module('BloggerCategory'));