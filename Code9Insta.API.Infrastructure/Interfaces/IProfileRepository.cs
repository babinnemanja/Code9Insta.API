using Code9Insta.API.Infrastructure.Entities;
using System;

namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IProfileRepository
    {
        bool Save();

        void CreateProfile(Profile profile);
        Profile GetProfile(Guid profileId);
        byte[] GetSaltByUserName(string userName);
        string GetUserNameById(Guid userId);
    }
}
