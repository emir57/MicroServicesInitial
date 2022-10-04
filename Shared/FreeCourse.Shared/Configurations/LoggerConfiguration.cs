using FreeCourse.Shared.CrossCuttingConcerns.Serilog;
using System;
using System.Diagnostics;

namespace FreeCourse.Shared.Configurations
{
    public class LoggerConfigurations : IDisposable
    {
        public LoggerServiceBase Logger { get; set; }

        public void Dispose()
        {
            Debug.WriteLine("Add successfully logger");
            GC.SuppressFinalize(this);
        }
    }
}
