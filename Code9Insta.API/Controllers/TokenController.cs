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
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IValidateRepository _validateRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly SymmetricSecurityKey _key;

        public TokenController(IValidateRepository validateRepository, IConfiguration configuration, IProfileRepository profileRepository, IAuthorizationManager authorizationManager)
        {
            _validateRepository = validateRepository;
            _profileRepository = profileRepository;
            _authorizationManager = authorizationManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
        }

        [Route("Request")]
        [HttpGet]
        public IActionResult RequestToken(string userName, string password)
        {
            //hash pasword
            var salt = _profileRepository.GetSaltByUserName(userName);
            var passwordHash = _authorizationManager.GeneratePasswordHash(password, salt);

            if (!_validateRepository.ValidateLogin(userName, passwordHash))
                return BadRequest("Could not verify username and password");

            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            _validateRepository.SaveRefreshToken(userName, refreshToken);

            if (!_validateRepository.Save())
            {
                return StatusCode(500, "There was a problem while handling your request.");
            }

            var token = _authorizationManager.GenerateToken(_key, userName);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken
            });

        }

        [Route("Refresh")]
        [HttpGet]
        public IActionResult RefreshToken(Guid userId, string refreshToken)
        {
            if (!_validateRepository.ValidateRefrashToken(userId, refreshToken))
                return BadRequest("Could not verify refresh token");

            var userName = _profileRepository.GetUserNameById(userId);

            var newRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            _validateRepository.SaveRefreshToken(userName, newRefreshToken);
         
            if (!_validateRepository.Save())
            {
                return StatusCode(500, "There was a problem while handling your request.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var token = _authorizationManager.GenerateToken(_key, userName);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = newRefreshToken
            });
        }
    }
}
