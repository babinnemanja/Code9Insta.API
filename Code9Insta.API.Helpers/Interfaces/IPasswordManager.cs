
namespace Code9Insta.API.Helpers.Interfaces
{
    public interface IPasswordManager
    {
        string GetPasswordHash(string password, byte[] salt);
    }
}
