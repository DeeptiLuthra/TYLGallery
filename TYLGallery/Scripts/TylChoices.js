//TylChoices.js

var mod = angular.module('TylMain');

mod.controller('ChoicesController',
    [
        "$scope", "$http", "AppNgConstants", "UserCookieService", function ($scope, $http, appNgConstants, UserCookieService) {
            $scope.showMsg = false;
            $scope.textMsg = "";
            $scope.choices = [];

            var url = appNgConstants.ChoicesApiUrl;
            $scope.getIconClass = function (feedback) {
                return (feedback === "Like" ? "glyphicon-thumbs-up" : "glyphicon-thumbs-down");
            }

            $scope.getTextClass = function(feedback) {
                return feedback === "Like" ? "text-primary" : "text-danger";
            }

            $scope.init = function () {
                var userId = UserCookieService.getUserIdFromCookie();
                var spinner = new Spinner().spin();
                $('#ChoicesDiv').prepend(spinner.el);
                $http.get(url + userId)
                    .then(function (response) {
                        if (response.data.length === 0) {
                            $scope.showMsg = true;
                            $scope.textMsg = "You have not made any choices yet!";
                            spinner.stop();
                        } else {
                            angular.copy(response.data, $scope.choices);
                            $scope.showMsg = false;
                            spinner.stop();
                        }
                    });
            }

        }
    ]);