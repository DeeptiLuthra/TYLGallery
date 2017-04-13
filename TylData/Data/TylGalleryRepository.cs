using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TylData.Models;
using TYLGallery.Common;
using System.Web.Http;

namespace TylData.Data
{
    public class TylGalleryRepository : ITylGalleryRepository
    {
        private TYLGalleryContext _ctx;

        public TylGalleryRepository(TYLGalleryContext ctx)
        {
            _ctx = ctx;
        }
        public bool AddPhoto(Image image)
        {
            try
            {
                _ctx.Images.Add(image);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Image GetImagesById(int? userId)
        {
            var random = new Random();

            int imageId = 0, randomNumber = 0;

            if (!_ctx.Images.Any())
            {
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
                    return null;
                }
                randomNumber = random.Next(0, validImageIds.Count() - 1);
                imageId = validImageIds.ToArray()[randomNumber];
            }
            else
            {
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
                    }
                    else
                    {
                        var user = _ctx.UserVisitedImages.FirstOrDefault(userRow => userRow.UserId == visitor.UserId && userRow.ImageId == visitor.ImageId);
                        if (user != null)
                        {

                            var exception = new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                            {
                                ReasonPhrase = ApplicationConstants.ExceptionCodes.AlreadySubmitted
                            });
                            throw exception;
                        }
                    }
                    visitor.VisitedOn = DateTime.Now.ToUniversalTime();
                    _ctx.UserVisitedImages.Add(visitor);

                    _ctx.SaveChanges();

                    dbContext.Commit();
                    return visitor.UserId;

                }
                catch (Exception ex)
                {
                    dbContext.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<UserVisitedImage> GetUserVisitedImages(int userId)
        {
            var choices = _ctx.UserVisitedImages.Include("Image").Where(item => item.UserId == userId);
            return choices;
        }
    }
}
