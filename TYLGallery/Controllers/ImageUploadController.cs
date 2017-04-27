using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Serilog;
using TylData.Data;
using TylData.Models;
using TYLGallery.Common;

namespace TYLGallery.Controllers
{
    [Authorize(Roles = ApplicationConstants.Keys.AdminRole)]
    public class ImageUploadController : ApiController
    {
        private readonly ITylGalleryRepository _repo;
        private ILogger _contextLogger;

        public ImageUploadController(ITylGalleryRepository repo)
        {
            _repo = repo;
            _contextLogger = Log.Logger.ForContext<ImageUploadController>();
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
                    _contextLogger.Information("Image:{Title} with encoding:{Encoding} is uploading to the database.",img.Title,img.Encoding);
                    _repo.AddPhoto(img);

                }

            }
            catch (System.Exception e)
            {
                var msg = e.Message;
                _contextLogger.Error(e,"Error occurred while uploading an image. Error Message:{ErrorMessage}",msg);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            _contextLogger.Information("Image successfully uploaded");
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}