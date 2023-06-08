/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postEditController', postEditController);

    postEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams']

    function postEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.post = {};

        $scope.chooseImage = () => {
            window.fileSelected = function (data) {
                $scope.$apply(() => {
                    $scope.post.image = data.path;
                })
            };
            window.open('/file-manager-elfinder', "Select", "menubar:0;");
        };

        $scope.ListCategories = [];
        $scope.getCategory = () => {
            apiService.get('/api/Categories/get-all-categories', null, (result) => {
                $scope.ListCategories = result.data;
            }, () => {
                alert('Get all list categories failed');
            });
        };
        $scope.getCategory();

        $scope.loadPostById = () => {
            apiService.get('/api/Posts/get-post-by-id/' + $stateParams.id, null, (result) => {
                $scope.post = result.data;

                const checkBoxList = document.querySelectorAll('.category_checkbox');
                checkBoxList.forEach(e => {
                    $scope.post.listCategoriesID.forEach(id => {
                        if (e.value == id) {
                            e.checked = true;
                        };
                    })
                });

            }, (error) => {
                notificationService.displayError('Không load post được');
            });
        };
        $scope.loadPostById();

        $scope.updatePost = () => {
            const checkBoxList = document.querySelectorAll('.category_checkbox');
            $scope.post.listCategoriesID = [];
            checkBoxList.forEach(e => {
                if (e.checked) {
                    $scope.post.listCategoriesID.push(e.value);
                };
            });

            apiService.put('/api/Posts/update-post/' + $stateParams.id, $scope.post, (result) => {
                notificationService.displaySuccess('Update post successfully');
                $state.go('post_list')
            }, (error) => {
                notificationService.displayError('Update post failed');
            });
        };
    };


})(angular.module('BloggerPost'));