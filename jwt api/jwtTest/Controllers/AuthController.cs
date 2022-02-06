using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jwtTest.configurations;
using jwtTest.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jwtTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> user;
        private readonly jwtConfig config;

        public AuthController(UserManager<IdentityUser> user, IOptions<jwtConfig> config)
        {
            this.user = user;
            this.config = config.Value;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto registerModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await user.FindByEmailAsync(registerModel.Email);
                if (existingUser != null)
                {
                    return BadRequest(new LoginResponse
                    {
                        Success = false,
                        Token = "",
                        Error = new List<string>() {
                        "Email already exists."
                    }
                    });
                }
                var registerUser = new IdentityUser() { UserName = registerModel.Email, Email = registerModel.Email };
                var IsCreated = await user.CreateAsync(registerUser, registerModel.password);
                if (!IsCreated.Succeeded)
                {
                    return BadRequest(new LoginResponse
                    {
                        Success = false,
                        Token = "",
                        Error = IsCreated.Errors.Select(x => x.Description).ToList()

                    });
                }
                else
                {
                    var jwtToken = GenerateJwtToken(registerUser);
                    return Ok(new LoginResponse { Success = true, Token = jwtToken });
                }
            }

            return BadRequest(new LoginResponse
            {
                Success = false,
                Token = "",
                Error = new List<string>() {
                        "Invalid payload"
                    }
            });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await user.FindByEmailAsync(loginModel.Email);

                if (existingUser == null)
                {
                    return BadRequest(new LoginResponse
                    {
                        Success = false,
                        Token = "",
                        Error = new List<string>() {
                        "Invalid login requests"
                    }
                    });
                }


                var isCorrect = await user.CheckPasswordAsync(existingUser, loginModel.password);

                if (!isCorrect)
                {
                    return BadRequest(new LoginResponse
                    {
                        Success = false,
                        Token = "",
                        Error = new List<string>() {
                        "Invalid password requests"
                    }
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);
                return Ok(new LoginResponse { Success = true, Token = jwtToken });

            }

            return BadRequest(new LoginResponse
            {
                Success = false,
                Token = "",
                Error = new List<string>() {
                        "Invalid payload"
                    }
            });

        }

        [NonAction]
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

    }
}