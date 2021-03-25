using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1
{
    public class Settings
    {
        public EnvironmentSettings EnvironmentSettings { get; set; }
    }

    public class EnvironmentSettings
    {
        public string Host { get; set; }
        public string SecretKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateAudience { get; set; }

    }
}
