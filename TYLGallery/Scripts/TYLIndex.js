var mod = angular.module('TylMain');
mod.requires = ['ngCookies'];


mod.controller('IndexController',
    [
        "$scope", "$http", "$cookies", "AppNgConstants", "ExceptionCode", function ($scope, $http, $cookies, appNgConstants, ExceptionCode) {
            $scope.imageSrc = "";
            $scope.isVisible = false;
            $scope.showMsg = false;
            $scope.textMessage = "";
            $scope.msgClass = "";
            $scope.areFeedbackOptionsVisible = false;

            $scope.GetImage = function (userId) {
                var spinner = new Spinner().spin();
                $('#IndexDiv').prepend(spinner.el);
                var url = userId == null ? appNgConstants.ShowImageUrl : appNgConstants.ShowImageUrl + "/" + userId;
                $scope.imageSrc = "";
                $scope.textMessage = "";
                $scope.showMsg = false;
                $scope.areFeedbackOptionsVisible = true;
                $http.get(url)
                    .then(function (response) {

                        if (response.data == null) {
                            $scope.isVisible = false;
                            $scope.showMsg = true;
                            $scope.msgClass = "bg-danger";
                            $scope.textMessage = "No images to show!";
                        } else {
                            $scope.isVisible = true;
                            $scope.showMsg = false;
                        }

                        $scope.imageSrc = response.data.ImageData;
                        $scope.imageId = response.data.Id;
                            spinner.stop();
                    },
                    function() {
                        spinner.stop();
                    });


            }

            $scope.SaveAction = function (userId, feedback) {
                var user = userId == null ? 0 : userId;
                var data = {
                    userId: user,
                    imageId: $scope.imageId,
                    feedback: feedback
                };
                $scope.showMsg = false;

                $http({
                    method: 'POST',
                    url: appNgConstants.ShowImageUrl,
                    dataType: 'json',
                    data: JSON.stringify(data),
                    headers: { 'Content-Type': 'application/json' }
                })
                    .then(function (response) {
                        $scope.showMsg = true;
                        $scope.textMessage = "Feedback received!";
                        $scope.msgClass = "bg-success";
                        $scope.areFeedbackOptionsVisible = false;
                        var date = new Date();
                        date.setFullYear(date.getFullYear() + 1);
                        //add userId to cookie
                        $cookies.put(appNgConstants.CookieKey,
                            response.data,
                            {
                                expires: date
                            });
                    },
                    function (response) {
                        $scope.showMsg = true;
                        $scope.msgClass = "bg-danger";
                        if (response.statusText === ExceptionCode) {
                            $scope.textMessage = "Feedback already submitted";
                        } else {
                            $scope.textMessage = "Error saving the feedback!";
                        }
                    });


            }
        }
    ]);