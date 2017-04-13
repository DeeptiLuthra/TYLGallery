var app = angular.module("TylMain");
app.requires = ["ngFileUpload"];
app.controller("UploadController",
    ["$scope", "$http", "$q", "AppNgConstants", "Upload", function ($scope, $http, $q, appNgConstants, upload) {



        $scope.file = null;

        $scope.UploadFile = function (file) {
            upload.upload({
                url: appNgConstants.UploadApiUrl,
                data: { file: file }
            })
                .then(function (response) {
                    alert("Uploaded!");
                },
                function (err) {
                    console.log("Error status: " + err.status);
                });
        };
    }
    ]);