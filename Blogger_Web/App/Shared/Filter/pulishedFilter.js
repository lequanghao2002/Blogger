/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.filter('statusFilter', () => {
        return (published) => {
            if (published == true) {
                return 'Yes';
            }
            else {
                return 'No';
            }
        }
    });

})(angular.module('BloggerCommon'));