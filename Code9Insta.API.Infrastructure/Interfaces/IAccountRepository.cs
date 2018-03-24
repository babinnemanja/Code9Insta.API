using Code9Insta.API.Infrastructure.Identity;

namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        bool Save();

        void CreateAccount(ApplicationUser applicationUser);
    }
}
