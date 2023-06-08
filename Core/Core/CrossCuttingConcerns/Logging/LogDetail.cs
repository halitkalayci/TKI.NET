﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }

}
