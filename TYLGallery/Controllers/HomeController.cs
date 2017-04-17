using System.Web.Mvc;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    public class HomeController : Controller
    {
        public string GetUserIdFromCookie()
        {
            var userCookie = Request.Cookies[ApplicationConstants.Keys.UserCookie];

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