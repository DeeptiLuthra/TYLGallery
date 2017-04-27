using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TylData.Data;
using TylData.Models;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    public class ShowPreferencesController : ApiController
    {
        private ITylGalleryRepository _repo;
        private ILogger _contextLogger;

        public ShowPreferencesController(ITylGalleryRepository repo)
        {
            _repo = repo;
            _contextLogger = Log.Logger.ForContext<ShowPreferencesController>();
            _contextLogger.Information("inside showPreference api");
        }

        // GET api/<controller>/5
        
        public IEnumerable<UserVisitedImage> Get(int id, int pageNum=0, int pageSize=0, bool byPage=true)
        {
            var pageNo = pageNum != 0 ? Convert.ToInt32(pageNum) : 1;
            var pageSz = pageSize != 0 ? Convert.ToInt32(pageSize) : ApplicationConstants.Paging.PageSize;
            _contextLogger.Information("pageno:{pageNum} and page size:{pageSize}", pageNum, pageSize);
            var choices = _repo.GetUserVisitedImages(id,pageNo,pageSz, byPage);
            _contextLogger.Information("(Get): Choices for user:{UserId} on page:{pageNum} are:{TotalVisitedImages}", id,pageNum, choices.Count());
            return choices;
        }

        [Route("api/ShowPreferences/UserRecordCount/{userId}")]
        [HttpGet]
        public int UserRecordCount(int userId)
        {
            var choices = _repo.GetUserVisitedImages(userId, 0, 0, false);
            _contextLogger.Information("(Get): Total choices for user:{UserId} are:{TotalVisitedImages}", userId, choices.Count());
            return choices.Count();
        }

    }
}