using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWaks.API.Models.DTO;
using NZWaks.API.Repositories;

namespace NZWaks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

       
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok(new { message = "User created successfully." });
                    }
                }
            }
            return BadRequest(identityResult);
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPassResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPassResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    //create token
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                        return Ok(jwtToken);
                    }

                    return Ok();
                    // return token 
                }
            }
            return BadRequest("Something went wrong. Incorrect password!");
        }
    }
}