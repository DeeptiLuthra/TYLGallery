using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace TylData.Data
{
    public class TylGalleryDataContextInitializer : DropCreateDatabaseIfModelChanges<TYLGalleryContext>
    {
        protected override void Seed(TYLGalleryContext context)
        {
            base.Seed(context);
            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin", string.Empty) + @"\App_Data";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir + @"\ImageTableScript.sql"));
        }
    }
}
