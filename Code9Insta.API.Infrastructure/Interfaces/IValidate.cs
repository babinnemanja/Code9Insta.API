namespace Code9Insta.API.Infrastructure.Interfaces
{
    public interface IValidate
    {
        bool ValidateLogin(string userName, string password);
    }
}
