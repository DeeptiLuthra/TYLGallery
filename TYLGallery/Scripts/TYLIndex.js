var mod = angular.module('TylMain');
mod.controller('IndexController',
    [
        "$scope", "$http", "AppNgConstants", "ExceptionCode", 'UserCookieService', function ($scope, $http, appNgConstants, ExceptionCode, UserCookieService) {
            $scope.imageSrc = "";
            $scope.isVisible = false;
            $scope.showMsg = false;
            $scope.textMessage = "";
            $scope.msgClass = "";
            $scope.areFeedbackOptionsVisible = false;
            $scope.UserId = UserCookieService.getUserIdFromCookie();
            $scope.GetImage = function () {
                var spinner = new Spinner().spin();
                $('#IndexDiv').prepend(spinner.el);
                var url = $scope.UserId == null ? appNgConstants.ShowImageUrl : appNgConstants.ShowImageUrl + "/" + $scope.UserId;
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
                            if (response.data != null) {
                                $scope.imageSrc = response.data.ImageData;
                                $scope.imageId = response.data.Id;
                            }
                            spinner.stop();
                    },
                    function() {
                        spinner.stop();
                    });


            }

            $scope.SaveAction = function (feedback) {
                var user = $scope.UserId == null ? 0 : $scope.UserId;
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
                        //add userId to cookie
                            UserCookieService.saveUserDetailsToCookie(response.data);
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