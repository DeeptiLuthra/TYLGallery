using System.Data.Entity;
using TylData.Models;

namespace TylData.Data
{
    public class TYLGalleryContext : DbContext
    {
        public TYLGalleryContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new TylGalleryDataContextInitializer());

        }

        public DbSet<UserVisitedImage> UserVisitedImages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }
}