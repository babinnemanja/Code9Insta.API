namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IValidateRepository
    {
        bool ValidateLogin(string userName, string password);
        bool IsUserNameHandleUnique(string userName, string handle);
    }
}
