using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using Serilog;

namespace TylData.Data
{
    public class TylGalleryDataContextInitializer : DbMigrationsConfiguration<TYLGalleryContext>
    {
        private ILogger _contextLogger;

        public TylGalleryDataContextInitializer()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            _contextLogger = Log.Logger.ForContext<TylGalleryDataContextInitializer>();
        }
        protected override void Seed(TYLGalleryContext context)
        {
#if DEBUG
            if (context.Images.Count() == 0)
            {
                try
                {
                    _contextLogger.Information("Seed: Seeding db context");
                    base.Seed(context);
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin", string.Empty) + @"\App_Data";
                    context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + @"\ImageTableScript.sql"));
                    _contextLogger.Information("Seed: Seeding completed!");
                }
                catch (Exception ex)
                {
                    _contextLogger.Error(ex, "Failed to seed database. Error description:{ErrorMessage}", ex.Message);
                }
            }
#endif
        }
    }
}
