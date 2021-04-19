using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DataProviderService
{
    public interface IDataProviderProfilerService
    {
        public ResultTime ComparePerformance();
    }
}
