using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TylData.Models;

namespace TylData.Data
{
    public interface ITylGalleryRepository
    {

        bool AddPhoto(Image image);
        int AddUserChoice(UserVisitedImage visitor);

        IEnumerable<UserVisitedImage> GetUserVisitedImages(int userId, int pageNum, int pageSize, bool byPage);
        Image GetImagesById(int? userId);
        int ImageCount { get; }
    }
}
