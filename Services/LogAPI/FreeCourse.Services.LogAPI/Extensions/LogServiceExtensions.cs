using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using FreeCourse.Shared.Exceptions.Logging;
using System.Diagnostics;

namespace FreeCourse.Services.LogAPI.Extensions;

public static class LogServiceExtensions
{
    public static IServiceCollection AddLogger(this IServiceCollection services, Action<LoggerConfiguration> action)
    {
        using LoggerConfiguration loggerSettings = new LoggerConfiguration();

        action(loggerSettings);

        LoggerNullException.ThrowIfNull(loggerSettings.Logger);
        WrongLoggingTypeException.ThrowIfWrongLoggingType(typeof(LoggerServiceBase), loggerSettings.Logger);

        services.AddScoped<LoggerServiceBase>(sp =>
        {
            return loggerSettings.Logger;
        });
        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, Func<Type> loggerFunc)
    {
        LoggerNullException.ThrowIfNull(loggerFunc());
        WrongLoggingTypeException.ThrowIfWrongLoggingType(typeof(LoggerServiceBase), loggerFunc());

        Type loggerType = loggerFunc();
        services.AddScoped(typeof(LoggerServiceBase), loggerType);
        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, Func<LoggerServiceBase> loggerFunc)
    {
        LoggerNullException.ThrowIfNull(loggerFunc());
        WrongLoggingTypeException.ThrowIfWrongLoggingType(typeof(LoggerServiceBase), loggerFunc());

        LoggerServiceBase logger = loggerFunc();
        services.AddScoped<LoggerServiceBase>(sp => logger);
        return services;
    }

    public class LoggerConfiguration : IDisposable
    {
        public LoggerServiceBase Logger { get; set; }

        public void Dispose()
        {
            Debug.WriteLine("Add successfully logger");
            GC.SuppressFinalize(this);
        }
    }
}
