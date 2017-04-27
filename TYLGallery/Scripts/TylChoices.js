//TylChoices.js

var mod = angular.module('TylMain');

//Controller
mod.controller('ChoicesController',
    [
        "$scope", "$http", "AppNgConstants", "UserCookieService", function ($scope, $http, appNgConstants, UserCookieService) {
            $scope.showMsg = false;
            $scope.textMsg = "";
            $scope.choices = [];
            $scope.NumOfPages = 0;
            $scope.pageSize = 4;
            $scope.pageNum = 2;

            $scope.getIconClass = function (feedback) {
                return (feedback === "Like" ? "glyphicon-thumbs-up" : "glyphicon-thumbs-down");
            }

            $scope.getTextClass = function(feedback) {
                return feedback === "Like" ? "text-primary" : "text-danger";
            }

            $scope.previousPage=function() {
                $scope.pageNum -= 1;
                $scope.init();
            }

            $scope.nextPage=function() {
                $scope.pageNum += 1;
                $scope.init();
            }

            $scope.init = function () {
                var userId = UserCookieService.getUserIdFromCookie();
                var spinner = new Spinner().spin();
                $('#ChoicesDiv').prepend(spinner.el);

                var userUrl = appNgConstants.UserRecordCountUrl;
                $http.get(userUrl + userId)
                    .then(function (response) {
                        $scope.NumOfPages = Math.ceil(response.data / $scope.pageSize);

                        $scope.pageNum = $scope.pageNum > $scope.NumOfPages ? $scope.NumOfPages : $scope.pageNum;
                        var choicesUrl = appNgConstants.ChoicesApiUrl + userId + "?pageNum=" + $scope.pageNum + "&pageSize=" + $scope.pageSize;

                if ($scope.NumOfPages > 0) {
                    $http.get(choicesUrl)
                        .then(function(choiceResponse) {
                            if (choiceResponse.data.length === 0) {
                                $scope.showMsg = true;
                                $scope.textMsg = "You have not made any choices yet!";
                                spinner.stop();
                            } else {
                                angular.copy(choiceResponse.data, $scope.choices);
                                $scope.showMsg = false;
                                spinner.stop();
                            }
                        });
                } else {
                    $scope.showMsg = true;
                    $scope.textMsg = "You have not made any choices yet!";
                    spinner.stop();
                }
                    });
            }

        }
    ]);