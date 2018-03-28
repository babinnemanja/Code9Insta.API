using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
