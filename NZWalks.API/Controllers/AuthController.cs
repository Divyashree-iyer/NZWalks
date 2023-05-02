using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Domain.DTO;
using NZWalks.Infrastructure.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            { 
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username,

            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was Registered! Please Login.");
                    }
                }

            }
            return BadRequest(identityResult.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO) 
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null) 
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (isPasswordValid)
                {
                    //get roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null) 
                    {
                        //Create Token
                        var jwtToken = _tokenRepository.CreateJWTtoken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);
                    }
                    return BadRequest("Something went Wrong!");
                }
                return BadRequest("Password is invalid!");
            }
            return BadRequest("Username is invalid.");
        }

    }
}
