using BusinessLogicLayer.DataProviderService;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1.Controllers
{
    [Route("data-provider")]
    [ApiController]
    public class DataProviderProfilerController : ControllerBase
    {
        private IDataProviderProfilerService _dataProviderProfilerService;
        public DataProviderProfilerController(IDataProviderProfilerService dataProviderProfilerService)
        {
            _dataProviderProfilerService = dataProviderProfilerService;
        }

        [HttpGet]
        [Route("get-result-time")]
        public ActionResult<ResultTime> GetResult()
        {
            var result = _dataProviderProfilerService.ComparePerformance();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();

        }
    }
}
