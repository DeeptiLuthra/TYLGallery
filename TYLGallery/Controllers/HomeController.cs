using System.Web.Mvc;
using Serilog;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _contextLogger;
        public HomeController()
        {
            Log.Logger = Logs.logger;
            _contextLogger = Log.Logger.ForContext<HomeController>();
            _contextLogger.Information("inside home");
        }
        public string GetUserIdFromCookie()
        {
            var userCookie = Request.Cookies[ApplicationConstants.Keys.UserCookie];
            _contextLogger.Information("GetUserIdFromCookie: Getting user cookie for user: {UserId}", userCookie?.Value);
            return userCookie?.Value;
        }

        public ActionResult Index()
        {

            return View();
        }

        [Authorize(Roles = ApplicationConstants.Keys.AdminRole)]
        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Choices()
        {
            return View();
        }

    }
}