/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postListController', postListController);

    postListController.$inject = ['$scope', 'apiService']

    function postListController($scope, apiService) {
        $scope.listPosts = [];

        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getListPosts = getListPosts;
        function getListPosts(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 2
                }
            };

            apiService.get('/api/Posts/get-list-posts', config, (result) => {
                $scope.listPosts = result.data.list;

                $scope.page = result.data.page;
                $scope.pagesCount = result.data.totalPages;
                $scope.totalCount = result.data.totalCounts;

            }, () => {
                alert('Get list posts failed');
            });
        }
        $scope.getListPosts();
    };


})(angular.module('BloggerPost'));