using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TylData.Data;
using TylData.Models;

namespace TYLGallery.Controllers
{
    public class ShowPreferencesController : ApiController
    {
        private ITylGalleryRepository _repo;

        public ShowPreferencesController(ITylGalleryRepository repo)
        {
            _repo = repo;
        }


        // GET api/<controller>/5
        public IEnumerable<UserVisitedImage> Get(int id)
        {
            var choices = _repo.GetUserVisitedImages(id);
            return choices;
        }

    }
}