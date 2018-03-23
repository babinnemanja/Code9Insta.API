using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Code9Insta.API.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Code9Insta.API.Controllers
{
    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        private readonly IValidate _validateRepository;
        private readonly IConfiguration _configuration;

        public TokensController(IValidate validateRepository, IConfiguration configuration)
        {
            _validateRepository = validateRepository;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken(string userName, string password)
        {
            if (!_validateRepository.ValidateLogin(userName, password))
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
