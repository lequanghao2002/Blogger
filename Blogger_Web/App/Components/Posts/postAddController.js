/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('postAddController', postAddController);

       //$state của ui-router (dùng để điều hướng)
    postAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']

    function postAddController($scope, apiService, notificationService, $state) {
        $scope.post = {
            ListCategoriesID: [],
            Published: true,
            UserID: "e1269ad1-2a5c-433c-8ad1-4542ad049cd1"
        };

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

        $scope.addPost = () => {
            const checkBoxList = document.querySelectorAll('.category_checkbox');
            checkBoxList.forEach(e => {
                if (e.checked) {
                    $scope.post.ListCategoriesID.push(e.value);
                };
            });

            apiService.post('/api/Posts/create-post', $scope.post, (result) => {
                notificationService.displaySuccess('Create post successfully');
                $state.go('post_list');
                

                apiService.get('/api/Posts/send-email', null, (result) => {

                }, (error) => {

                });
            }, (error) => {
                notificationService.displayError('Create post failed');
            });
        };
    };


})(angular.module('BloggerPost'));