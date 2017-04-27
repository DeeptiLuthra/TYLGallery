"use strict";
angular.module("TylMain", ["ngCookies"])
    .constant("AppNgConstants",
    {
        "ChoicesApiUrl": "/api/ShowPreferences/",
        "UserRecordCountUrl": "/api/ShowPreferences/UserRecordCount/",
        "UploadApiUrl": "/api/ImageUpload/",
        "ShowImageUrl": "/api/ShowImage",
        "CookieKey": "UserCookieKey"
    })
    .constant("ExceptionCode", "AlreadySubmitted")
    .factory('UserCookieService', ['$cookies', 'AppNgConstants', function ($cookies, appNgConstants) {
        var factory = {};

           factory.getUserIdFromCookie = function() {
                var userCookie = $cookies.get(appNgConstants.CookieKey);

                return userCookie != null ? parseInt(userCookie) : null;
            };
            factory.saveUserDetailsToCookie = function(userId) {
                var date = new Date();
                date.setFullYear(date.getFullYear() + 1);

                $cookies.put(appNgConstants.CookieKey,
                    userId,
                    {
                        expires: date
                    });
           };
            return factory;
        }])
    ;

