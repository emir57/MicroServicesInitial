using FreeCourse.Shared.CrossCuttingConcerns.Serilog.ConfigurationModels;
using FreeCourse.Shared.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IO;

namespace FreeCourse.Shared.CrossCuttingConcerns.Serilog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        private IConfiguration _configuration;
        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                                                        .Get<FileLogConfiguration>() ??
                                                        throw new System.ArgumentNullException(SerilogMessages.NullOptionMessage);

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

            Logger = new LoggerConfiguration()
                .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 500000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
