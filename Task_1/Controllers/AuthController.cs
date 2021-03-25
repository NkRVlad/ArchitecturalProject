using BusinessLogicLayer.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Settings _settings;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _settings = new Settings();
            _userService = userService;
            _settings = new Settings();
            configuration.Bind(_settings);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login userLogin)
        {
            if(userLogin == null)
            {
                return BadRequest("Invalid data");
            }
            var user = _userService.GetAll();

            bool flagStatusOk = false;

            foreach (var tempUser in user)
            {
                if (userLogin.UserName == tempUser.Login && userLogin.Password == tempUser.Password)
                {
                    flagStatusOk = true; 
                    break;
                }
            }

            if(flagStatusOk)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.EnvironmentSettings.SecretKey));

                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOption = new JwtSecurityToken(

                    issuer: _settings.EnvironmentSettings.Host,
                    audience: _settings.EnvironmentSettings.Host,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
