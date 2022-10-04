using Microsoft.Extensions.Configuration;
using Serilog;

namespace FreeCourse.Shared.CrossCuttingConcerns.Serilog.Loggers
{
    public class SeqLogger : LoggerServiceBase
    {
        private readonly IConfiguration _configuration;
        public SeqLogger(IConfiguration configuration)
        {
            _configuration = configuration;

            Logger = new LoggerConfiguration()
                .WriteTo.Seq(_configuration["SeriLogConfigurations:SeqLogConfiguration:Host"].ToString())
                .CreateLogger();
        }
    }
}
