using AuthorizationAPI.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IConfiguration _config;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManger = roleManager;
            _config = configuration;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            var user = await _userManager.FindByNameAsync(register.UserName);
            if(user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response {Status="Error", Messages="User Already Exists." });
            }

            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(applicationUser, register.Password);

            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "error", Messages = "something went wrong." });
            }

            return Ok(new Response { Status = "success", Messages = "user created successfully." });
        }
    }
}
