using FreeCourse.Services.LogAPI.Serilog.ConfigurationModels;
using FreeCourse.Services.LogAPI.Serilog.Messages;
using Serilog;

namespace FreeCourse.Services.LogAPI.Serilog.Logger;

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
