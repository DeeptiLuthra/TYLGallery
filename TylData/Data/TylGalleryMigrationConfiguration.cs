using System.Data.Entity.Migrations;

namespace TylData.Data
{
    public class TylGalleryMigrationConfiguration : DbMigrationsConfiguration<TYLGalleryContext>
    {
        public TylGalleryMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }
    }
}