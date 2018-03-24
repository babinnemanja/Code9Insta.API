using Code9Insta.API.Infrastructure.Data;
using Code9Insta.API.Infrastructure.Interfaces;
using System.Linq;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class ValidateRepository : IValidate
    {
        private readonly CodeNineDbContext _context;

        public ValidateRepository(CodeNineDbContext context)
        {
            _context = context;
        }

        public bool ValidateLogin(string userName, string password)
        {
            return _context.Users.Any(a => a.UserName == userName && a.PasswordHash == password);
        }
    }
}
