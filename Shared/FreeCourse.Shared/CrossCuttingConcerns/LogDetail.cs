using System;
using System.Collections.Generic;

namespace FreeCourse.Shared.CrossCuttingConcerns
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
