var app = angular.module("TylMain");
app.requires = ["ngFileUpload"];
app.controller("UploadController",
    ["$scope", "$http", "$q", "AppNgConstants", "Upload", function ($scope, $http, $q, appNgConstants, upload) {
        $scope.messageClass = "";
        $scope.textMessage = "";
        
        $scope.isUploading = false;
        $scope.file = null;

            $scope.resetFile = function() {
                $scope.textMessage = "";
            };

        $scope.UploadFile = function (file) {
            $scope.isUploading = true;
            $scope.textMessage = "";
            var spinner = new Spinner();
            spinner.spin();
            $("#uploadDiv").append(spinner.el);

            upload.upload({
                url: appNgConstants.UploadApiUrl,
                data: { file: file }
            })
                .then(function (response) {
                    $scope.messageClass = "text-success";
                            $scope.textMessage = "File uploaded successfully!";
                        },
                function (err) {
                    $scope.messageClass = "text-danger";
                    $scope.textMessage = "Error encountered while uploading the file..";
                    console.log("Error status: " + err.status);
                })
                .then(function() {
                    spinner.stop();
                        $scope.isUploading = false;
                    })
                ;
        };
    }
    ]);