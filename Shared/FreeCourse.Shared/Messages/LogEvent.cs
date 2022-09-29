using FreeCourse.Shared.CrossCuttingConcerns;
using System.Collections.Generic;

namespace FreeCourse.Shared.Messages
{
    public sealed class LogEvent
    {
        public string MethodName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
