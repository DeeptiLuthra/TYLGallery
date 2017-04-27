using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TylData.Models;
using TYLGallery.Common;
using System.Web.Http;
using Serilog;

namespace TylData.Data
{
    public class TylGalleryRepository : ITylGalleryRepository
    {
        private TYLGalleryContext _ctx;
        private ILogger _contextLogger;

        public TylGalleryRepository(TYLGalleryContext ctx)
        {
            _ctx = ctx;
            _contextLogger = Log.Logger.ForContext<TylGalleryRepository>();
        }
        public bool AddPhoto(Image image)
        {
            try
            {
                Image img=_ctx.Images.Add(image);
                _ctx.SaveChanges();
                _contextLogger.Information("AddPhoto: New Image added with id:{ImageId}", img.Id);
                return true;
            }
            catch (Exception ex)
            {

                _contextLogger.Error(ex, "AddPhoto: Failed to save the image:{Title} of type:{Encoding} to the database.", image.Title,image.Encoding);
                return false;
            }
        }

        public Image GetImagesById(int? userId)
        {
            var random = new Random();

            int imageId = 0, randomNumber = 0;

            if (!_ctx.Images.Any())
            {
                _contextLogger.Warning("GetImagesById: No images are there in the database.");
                return null;
            }
            var imageIds = (from m in _ctx.Images
                            select m.Id);
            if (userId.HasValue)
            {
                var userRespondedImageIds = (from v in _ctx.UserVisitedImages
                                             where v.UserId == userId
                                             select v.ImageId);

                var validImageIds = imageIds.Where(id => !userRespondedImageIds.Contains(id));

                if (!validImageIds.Any())
                {
                    _contextLogger.Information("GetImagesById: User:{UserId} has given feedback on all the images", userId);
                    return null;
                }
                randomNumber = random.Next(0, validImageIds.Count() - 1);
                imageId = validImageIds.ToArray()[randomNumber];
            }
            else
            {
                _contextLogger.Information("GetImagesById: Displaying randomized images to a new user");
                randomNumber = random.Next(0, ImageCount - 1);
                imageId = imageIds.ToArray()[randomNumber];
            }

            return _ctx.Images.FirstOrDefault(image => image.Id == imageId);

        }

        public int ImageCount => _ctx.Images.Count();

        public int AddUserChoice(UserVisitedImage visitor)
        {
            using (var dbContext = _ctx.Database.BeginTransaction())
            {
                try
                {
                    if (visitor.UserId == 0)
                    {
                        var user = new User();
                        user = _ctx.Users.Add(user);
                        _ctx.SaveChanges();
                        visitor.UserId = user.Id;

                        _contextLogger.Information("AddUserChoice: New user added with id:{UserId}", user.Id);
                    }
                    else
                    {
                        var user = _ctx.UserVisitedImages
                            .FirstOrDefault(userRow => userRow.UserId == visitor.UserId && userRow.ImageId == visitor.ImageId);
                        if (user != null)
                        {
                            var exception = new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                            {
                                ReasonPhrase = ApplicationConstants.ExceptionCodes.AlreadySubmitted
                            });
                            _contextLogger.Warning(exception, "AddUserChoice: User:{UserId} has already submitted feedback for the image:{ImageId}"
                                                                        , user.Id, visitor.ImageId);
                            throw exception;
                        }
                    }
                    visitor.VisitedOn = DateTime.Now.ToUniversalTime();
                    _ctx.UserVisitedImages.Add(visitor);
                    _ctx.SaveChanges();
                    _contextLogger.Information("AddUserChoice: User:{UserId} has provided feedback:{feedback} for image:{ImageId}"
                                                , visitor.UserId,visitor.Feedback,visitor.ImageId);
                    dbContext.Commit();
                    return visitor.UserId;

                }
                catch (Exception ex)
                {
                    _contextLogger.Error(ex, "AddUserChoice: Failed to register feedback for user:{UserId} on image:{ImageId}"
                        , visitor.UserId,visitor.ImageId);
                    dbContext.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<UserVisitedImage> GetUserVisitedImages(int userId, int pageNum, int pageSize, bool byPage=true)
        {
            List<UserVisitedImage> choices;
            if (byPage)
            {
                 choices =
                    _ctx.UserVisitedImages.Include("Image")
                        .Where(item => item.UserId == userId)
                        .OrderByDescending(order => order.VisitedOn)
                        .Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                 choices =
                    _ctx.UserVisitedImages
                        .Where(item => item.UserId == userId)
                        .ToList();
            }
            _contextLogger.Information("GetUserVisitedImages: Total images fetched for user:{UserId} are:{TotalVisitedImages}",userId,choices.Count());
            return choices;
        }
    }
}
