using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TylData.Data;
using TylData.Models;

namespace TYLGallery.Controllers
{
    public class ShowImageController : ApiController
    {
        private ITylGalleryRepository _repo;

        public ShowImageController(ITylGalleryRepository repo)
        {
            _repo = repo;
        }

        private Image GetImage(int? userId)
        {
            return _repo.GetImagesById(userId);

        }

        // GET api/<controller>
        public Image Get()
        {
            return GetImage(null);
        }

        // GET api/<controller>/5
        public Image Get(int id)
        {
            return GetImage(id);
        }

        // POST api/<controller>
        public async Task<int> Post(UserVisitedImage visitor)
        {
            return _repo.AddUserChoice(visitor);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}