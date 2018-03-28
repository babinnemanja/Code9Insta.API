using Code9Insta.API.Core.DTO;
using Code9Insta.API.Helpers.Interfaces;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Code9Insta.API.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IValidateRepository _validateRepository;
        private readonly IPasswordManager _passwordManager;

        public ProfileController(IProfileRepository profileRepository, IValidateRepository validateRepository, IPasswordManager passwordManager)
        {
            _profileRepository = profileRepository;
            _validateRepository = validateRepository;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateProfile([FromBody]ProfileDto profile)
        {
            if(!_validateRepository.IsUserNameHandleUnique(profile.User.UserName, profile.Handle))
            {
                return StatusCode(409, "User allready exists"); 
            }

            var prof = AutoMapper.Mapper.Map<Profile>(profile);
            var salt = new byte[128 / 8];

            prof.User.PasswordHash = _passwordManager.GetPasswordHash(profile.User.Password, salt);
            prof.User.Salt = salt;

            _profileRepository.CreateProfile(prof);

            if(!_profileRepository.Save())
            {
                return StatusCode(500, "There was a problem while handling your request.");
            }

            return StatusCode(200, "Profile created");
        }

        [AllowAnonymous]
        [HttpGet("{profileId}")]
        public IActionResult GetProfile(Guid profileId)
        {
            var profile = _profileRepository.GetProfile(profileId);

            if (profile == null)
            {
                return NotFound();
            }

            var profileDto = AutoMapper.Mapper.Map<ProfileDto>(profile);

            return Ok(profileDto);
        }
    }
}
