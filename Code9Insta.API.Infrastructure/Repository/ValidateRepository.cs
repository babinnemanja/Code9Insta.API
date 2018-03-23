using Code9Insta.API.Core.Interfaces;

namespace Code9Insta.API.Infrastructure.Repository
{
    public class ValidateRepository : IValidate
    {
        public bool ValidateLogin(string userName, string password)
        {
            return true;
        }
    }
}
