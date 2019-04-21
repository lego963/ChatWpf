using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface IClientDataStore
    {
        System.Threading.Tasks.Task<bool> HasCredentialsAsync();

        System.Threading.Tasks.Task EnsureDataStoreAsync();

        System.Threading.Tasks.Task<LoginCredentialsDataModel> GetLoginCredentialsAsync();

        System.Threading.Tasks.Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials);
    }
}