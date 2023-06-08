/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('roleEditController', roleEditController);

    roleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function roleEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.role = {};

        $scope.loadRoleById = () => {
            apiService.get('api/Roles/get-role-by-id/' + $stateParams.id, null, (result) => {
                $scope.role = result.data;
            }, (error) => {
                notificationService.displayError('Không load role được');
            });
        }
        $scope.loadRoleById();

        $scope.editRole = () => {
            apiService.put('/api/Roles/update-role/' + $stateParams.id, $scope.role, (result) => {
                notificationService.displaySuccess('Update role successfully');
                $state.go('role_list');
            }, (error) => {
                notificationService.displayError('Update role failed');
            });
        }
    };

})(angular.module('BloggerRole'))