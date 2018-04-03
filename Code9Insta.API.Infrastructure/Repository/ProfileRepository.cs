using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CodeNineDbContext _context;

        public ProfileRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public void CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);           
        }

        public Profile GetProfile(Guid profileId)
        {
           return _context.Profiles.FirstOrDefault(f => f.Id == profileId);
        }

        public byte[] GetSaltByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(f => f.UserName == userName).Salt;
        }

        public ApplicationUser GetUserInfo(Guid userId)
        {
            return _context.Users.FirstOrDefault(f => f.Id == userId);
        }

        public Guid GetUserIdByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(f => f.UserName == userName).Id;
        }

        public Guid GetProfileIdByUserId(Guid userId)
        {
            return _context.Users.FirstOrDefault(f => f.Id == userId).Id;
        }

        public Profile GetProfileByHandle(string handle)
        {
            return _context.Profiles.FirstOrDefault(f => f.Handle == handle);
        }

        public List<Profile> GetAllProfiles()
        {
            return _context.Profiles.ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
