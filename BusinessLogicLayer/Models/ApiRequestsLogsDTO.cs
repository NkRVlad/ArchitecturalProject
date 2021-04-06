using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Models
{
    public class ApiRequestsLogsDTO
    {
        public string ResourceName { get; set; }
        public string EndpointName { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
