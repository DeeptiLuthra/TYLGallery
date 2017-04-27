using System;
using System.Collections.Generic;
using System.Linq;

namespace TYLGallery.Common
{
    public class ApplicationConstants
    {
        public class Keys
        {
            public const string UserCookie = "UserCookieKey";
            public const string UserId = "UserId";
            public const string AdminRole = "Admin";
            public const string ExceptionCode = "ExceptionCodeKey";
        }

        public class Paging
        {
            public const int PageSize = 3;
        }

        public class ExceptionCodes
        {
            public const string AlreadySubmitted = "AlreadySubmitted";
        }

    }
}