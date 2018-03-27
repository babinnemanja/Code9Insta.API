using Code9Insta.API.Core.DTO;
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

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateProfile([FromBody]ProfileDto profile)
        {

            _profileRepository.CreateProfile(AutoMapper.Mapper.Map<Profile>(profile));

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
