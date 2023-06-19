/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('categoryListController', categoryListController);

    categoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox']

    function categoryListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.listCategories = [];

        $scope.page = 0;
        $scope.pageSize = 6;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.num = 0;

        $scope.getListCategories = getListCategories;
        function getListCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 6
                }
            };

            apiService.get('/api/Categories/get-list-categories', config, (result) => {
                $scope.listCategories = result.data.list;

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
                //alert('Get list categories failed');
            });
        }
        $scope.getListCategories();

        $scope.deleteCategory = (id) => {
            $ngBootbox.confirm('Do you want to delete category with id = ' + id).then(() => {
                apiService.delete('/api/Categories/delete-category/' + id, null, () => {
                    notificationService.displaySuccess('Delete category successfully');
                    $scope.getListCategories();
                }, () => {
                    notificationService.displayError('Delete category failed');
                })
            });
        };

    };
})(angular.module('BloggerCategory'));