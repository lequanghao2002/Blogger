/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('roleListController', roleListController);

    roleListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox']

    function roleListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.listRoles = [];

        $scope.page = 0;
        $scope.pageSize = 6;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.num = 0;

        $scope.getListRoles = getListRoles;
        function getListRoles(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 6
                }
            };

            apiService.get('/api/Roles/get-list-roles', config, (result) => {
                $scope.listRoles = result.data.list;

                $scope.page = result.data.page;
                $scope.num = result.data.count;
                $scope.pagesCount = result.data.pagesCount;
                $scope.totalCount = result.data.totalCount;

                $scope.showTo = ($scope.page * $scope.pageSize + 1);
                $scope.showFrom = ($scope.page * $scope.pageSize) + $scope.num;

                if ($scope.showFrom % $scope.pageSize == 1) {
                    $scope.showEnd = true;
                }
                else {
                    $scope.showEnd = false;
                }
            }, () => {
                alert('Get list roles failed');
            });
        }
        $scope.getListRoles();

        $scope.deleteRole = (id) => {
            $ngBootbox.confirm('Do you want to delete role with id = ' + id).then(() => {
                apiService.delete('/api/Roles/delete-role/' + id, null, () => {
                    notificationService.displaySuccess('Delete role successfully');
                    $scope.getListRoles();
                }, () => {
                    notificationService.displayError('Delete role failed');
                })
            });
        };

    };
})(angular.module('BloggerRole'));