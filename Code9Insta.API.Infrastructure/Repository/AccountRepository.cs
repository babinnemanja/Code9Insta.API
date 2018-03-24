using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Identity;
using Code9Insta.API.Infrastructure.Interfaces;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CodeNineDbContext _context;

        public AccountRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(ApplicationUser applicationUser)
        {
            _context.Users.Add(applicationUser);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
