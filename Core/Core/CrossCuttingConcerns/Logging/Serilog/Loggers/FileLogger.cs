using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger()
        {
            // loglama yapacağım dosyayı bulmak
            string path = Directory.GetCurrentDirectory() + "/logs/log.txt";
            Logger = new LoggerConfiguration().WriteTo.File(
                path,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 500000,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message}{NewLine}{Exception}"
                ).CreateLogger();
        }
    }
}
