using System.Threading.Tasks;
using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.DI.Interfaces
{
    public interface IClientDataStore
    {
        Task<bool> HasCredentialsAsync();

        System.Threading.Tasks.Task EnsureDataStoreAsync();

        Task<LoginCredentialsDataModel> GetLoginCredentialsAsync();

        System.Threading.Tasks.Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials);

        System.Threading.Tasks.Task ClearAllLoginCredentialsAsync();
    }
}
