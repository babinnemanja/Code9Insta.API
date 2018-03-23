namespace Code9Insta.API.Core.Interfaces
{
    public interface IValidate
    {
        bool ValidateLogin(string userName, string password);
    }
}
