using JWTAuthorization.BL;
using JWTAuthorization.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace JWTAuthurization.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configration;

        public UserController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configration = configuration;
        }

        [HttpPost]
        public ActionResult<string> Register(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = new MailAddress(registerDTO.Email).User
            };

            var result = _userManager.CreateAsync(user, registerDTO.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,registerDTO.Email),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, nameof(Roles.User)),
                new Claim(ClaimTypes.Country , "Egypt")
            };

            var clamisResult = _userManager.AddClaimsAsync(user, claims).Result;

            return Ok("done");
        }

        [HttpPost]

        public async Task<ActionResult<TokenDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return BadRequest("User not Found");

            if (await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Try Agian Later ");
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                await _userManager.AccessFailedAsync(user);
                return BadRequest("Incorrct Password");
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var expire = DateTime.Now.AddDays(1);
            var getKey = _configration.GetValue<string>("Key");
            var keyInbyte = Encoding.ASCII.GetBytes(getKey);
            var secretKey = new SymmetricSecurityKey(keyInbyte);

            var userCredintails = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: userCredintails,
                audience: "MyAudicne",
                issuer: "Me",
                expires: expire
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenDTO
            {
                Token = tokenString,
                Expire = expire
            };
        }


        [HttpPost]
        public ActionResult<CreateAdminDTO> CreateAdmin(CreateAdminDTO createAdminDTO)
        {
            var user = new ApplicationUser
            {
                Email = createAdminDTO.Email,
                UserName = new MailAddress(createAdminDTO.Email).User
            };

            var result = _userManager.CreateAsync(user, createAdminDTO.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,createAdminDTO.Email),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, nameof(Roles.Admin)),
                new Claim(ClaimTypes.Country , "Egypt")
            };

            var clamisResult = _userManager.AddClaimsAsync(user, claims).Result;

            return Ok("done");
        }



    }
}
