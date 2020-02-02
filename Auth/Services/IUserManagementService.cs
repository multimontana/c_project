namespace Auth.Services
{
    using Auth.Model;

    public interface IUserManagementService
    {
        bool IsValidUser(string username, byte[] password, AuthenticateModel requestModel);
    }
}