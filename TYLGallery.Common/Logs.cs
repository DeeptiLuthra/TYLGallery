using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace TYLGallery.Common
{
    public static class Logs
    {
        public static ILogger logger;
        static Logs()
        {
            logger = new LoggerConfiguration().WriteTo.Seq("http://localhost:5341/").CreateLogger();
        }
    }
}
