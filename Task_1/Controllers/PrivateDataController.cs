using BusinessLogicLayer.FilterService;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1.Controllers
{
    [Route("privatedata")]
    [ApiController]
    public class PrivateDataController : ControllerBase
    {
        private IUserService _userService;
        public PrivateDataController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet]
        [Route("get-user")]
        [Authorize]
        [ServiceFilter(typeof(ApiRequestFilter))]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetAll();
        }
    }
}
