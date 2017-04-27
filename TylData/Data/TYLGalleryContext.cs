using System.Data.Entity;
using TylData.Models;
using Serilog;
using TYLGallery.Common;

namespace TylData.Data
{
    public class TYLGalleryContext : DbContext
    {
        private ILogger _contextLogger;

        public TYLGalleryContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            _contextLogger = Log.Logger.ForContext<TYLGalleryContext>();
            _contextLogger.Information("TYLGalleryContext: Connecting to database:{dbConnection}", "DefaultConnection");
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TYLGalleryContext, TylGalleryDataContextInitializer>());
        }

        public DbSet<UserVisitedImage> UserVisitedImages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }
}