/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postListController', postListController);

    postListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox']

    function postListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.listPosts = [];

        $scope.page = 0;
        $scope.pageSize = 6;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.num = 0;
        
        $scope.dateFrom;
        $scope.dateTo;
        $scope.classify;
        $scope.filter;

        $scope.listCategories = [];
        $scope.getListCategory = () => {
            apiService.get('/api/Categories/get-all-categories', null, (result) => {
                $scope.listCategories = result.data;
            }, () => {
                //alert('Get all list categories failed');
            });
        };
        $scope.getListCategory();

        $scope.getListPosts = getListPosts;
        function getListPosts(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: $scope.pageSize,
                    dateFrom: $scope.dateFrom,
                    dateTo: $scope.dateTo,
                    classify: $scope.classify,
                    filter: $scope.filter
                }
            };

            apiService.get('/api/Posts/get-list-posts', config, (result) => {
                $scope.listPosts = result.data.list;
                $scope.page = result.data.page;
                $scope.num = result.data.count;
                $scope.pagesCount = result.data.pagesCount;
                $scope.totalCount = result.data.totalCount;

                if ($scope.num == 0) {
                    $scope.showTo = 0;
                }
                else {
                    $scope.showTo = ($scope.page * $scope.pageSize + 1);
                }
                $scope.showFrom = ($scope.page * $scope.pageSize) + $scope.num;

                if ($scope.showFrom % $scope.pageSize == 1) {
                    $scope.showEnd = true;
                }
                else {
                    $scope.showEnd = false;
                }

             
            }, () => {
                //alert('Get list posts failed');
            });
        }
        $scope.getListPosts();

        $scope.deletePost = (id) => {
            $ngBootbox.confirm('Do you want to delete post with id = ' + id).then(() => {
                apiService.delete('/api/Posts/delete-post/' + id, null, () => {
                    notificationService.displaySuccess('Delete post successfully');
                    $scope.getListPosts();
                }, () => {
                    notificationService.displayError('Delete post failed');
                })
            });
        };
    };


})(angular.module('BloggerPost'));