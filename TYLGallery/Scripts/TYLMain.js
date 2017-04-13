"use strict";
angular.module("TylMain", ["ngFileUpload"])
    .constant("AppNgConstants",
    {
        "ChoicesApiUrl": "/api/ShowPreferences/",
        "UploadApiUrl": "/api/ImageUpload/",
        "ShowImageUrl": "/api/ShowImage",
        "CookieKey": "UserCookieKey"
    })
    .constant("ExceptionCode", "AlreadySubmitted");

