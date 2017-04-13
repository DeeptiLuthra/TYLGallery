using System.Web.Mvc;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var userCookie = Request.Cookies[ApplicationConstants.Keys.UserCookie];

            if (userCookie != null)
            {
                Session[ApplicationConstants.Keys.UserId] = userCookie.Value;
            }

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