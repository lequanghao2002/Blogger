/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postListController', postListController);

    postListController.$inject = ['$scope', 'apiService']

    function postListController($scope, apiService) {
        $scope.listPosts = [];

        $scope.page = 0;
        $scope.pageSize = 6;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.num = 0;

        $scope.getListPosts = getListPosts;
        function getListPosts(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 6
                }
            };

            apiService.get('/api/Posts/get-list-posts', config, (result) => {
                $scope.listPosts = result.data.list;

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
                alert('Get list posts failed');
            });
        }
        $scope.getListPosts();
    };


})(angular.module('BloggerPost'));