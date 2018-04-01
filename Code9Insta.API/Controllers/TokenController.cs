using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Code9Insta.API.Helpers.Interfaces;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Code9Insta.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IValidateRepository _validateRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordManager _passwordManager;

        public TokenController(IValidateRepository validateRepository, IConfiguration configuration, IProfileRepository profileRepository, IPasswordManager passwordManager)
        {
            _validateRepository = validateRepository;
            _profileRepository = profileRepository;
            _configuration = configuration;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RequestToken(string userName, string password)
        {
            //hash pasword
            var salt = _profileRepository.GetSaltByUserName(userName);
            var passwordHash = _passwordManager.GetPasswordHash(password, salt);

            if (!_validateRepository.ValidateLogin(userName, passwordHash))
                return BadRequest("Could not verify username and password");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "code9.com",
                audience: "code9.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }
    }
}
