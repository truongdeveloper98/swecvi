using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractDicomConsole
{
    public static class Logging
    {
        public static void Configure()
        {
            ILogger logger = new LoggerConfiguration()
              .MinimumLevel.Verbose()
              .WriteTo.File(path: "log.txt")
              .CreateLogger();
            Log.Logger = logger;

        }
    }
}
