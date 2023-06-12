/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postDetailController', postDetailController);

    postDetailController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams']

    function postDetailController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.post = [];

        $scope.loadPostById = () => {
            apiService.get('/api/Posts/get-post-by-id/' + $stateParams.id, null, (result) => {
                $scope.post = result.data;

                document.querySelector('#content').innerHTML = $scope.post.content;
            }, (error) => {
                notificationService.displayError('Không load post được');
            });
        };
        $scope.loadPostById();
    }


})(angular.module('BloggerPost'));