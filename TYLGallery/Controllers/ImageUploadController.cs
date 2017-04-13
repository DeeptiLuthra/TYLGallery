using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TylData.Data;
using TylData.Models;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    [Authorize(Roles = ApplicationConstants.Keys.AdminRole)]
    public class ImageUploadController : ApiController
    {
        private readonly ITylGalleryRepository _repo;

        public ImageUploadController(ITylGalleryRepository repo)
        {
            _repo = repo;
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post()
        {
            Image img;
            try
            {
                var multipartFile = await Request.Content.ReadAsMultipartAsync();

                foreach (var file in multipartFile.Contents)
                {
                    img = new Image()
                    {
                        Title = file.Headers.ContentDisposition.FileName,
                        Encoding = file.Headers.ContentType.MediaType,
                        ImageBytes = await file.ReadAsByteArrayAsync()
                    };
                    _repo.AddPhoto(img);

                }

            }
            catch (System.Exception e)
            {
                var msg = e.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}