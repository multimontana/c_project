namespace InfraManager.WebApi.Auth.Services
{
    using InfraManager.WebApi.Auth.Model;

    public interface IUserManagementService
    {
        bool IsValidUser(string username, byte[] password, AuthenticateModel requestModel);
    }
}