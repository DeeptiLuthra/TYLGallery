//TylChoices.js

var mod = angular.module('TylMain');
mod.requires = [];


mod.controller('ChoicesController',
    [
        "$scope", "$http", "AppNgConstants", function ($scope, $http, appNgConstants) {
            $scope.showMsg = false;
            $scope.textMsg = "";
            $scope.choices = [];

            var url = appNgConstants.ChoicesApiUrl;
            $scope.getIconClass = function (feedback) {
                return (feedback === "Like" ? "glyphicon-thumbs-up" : "glyphicon-thumbs-down");
            }

            $scope.init = function (userId) {
                $http.get(url + userId)
                    .then(function (response) {
                        if (response.data == null) {
                            $scope.showMsg = true;
                            $scope.textMsg = "You have not made any choices yet!";
                        } else {
                            angular.copy(response.data, $scope.choices);
                            $scope.showMsg = false;
                        }
                    });
            }

        }
    ]);