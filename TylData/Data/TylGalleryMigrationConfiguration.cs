using System;
using System.Data.Entity.Migrations;
using System.IO;

namespace TylData.Data
{
    public class TylGalleryMigrationConfiguration : DbMigrationsConfiguration<TYLGalleryContext>
    {
        public TylGalleryMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TYLGalleryContext context)
        {
            base.Seed(context);
            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin", string.Empty) + @"\App_Data";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + @"\ImageTableScript.sql"));
        }
    }
}