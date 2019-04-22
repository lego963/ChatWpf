using System.Linq;
using System.Threading.Tasks;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.DI.Interfaces;

namespace ChatWpf.Relational
{
    public class BaseClientDataStore : IClientDataStore
    {
        private readonly ClientDataStoreDbContext _dbContext;

        public BaseClientDataStore(ClientDataStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> HasCredentialsAsync()
        {
            return await GetLoginCredentialsAsync() != null;
        }

        public async Task EnsureDataStoreAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            return Task.FromResult(_dbContext.LoginCredentials.FirstOrDefault());
        }

        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            _dbContext.LoginCredentials.RemoveRange(_dbContext.LoginCredentials);

            _dbContext.LoginCredentials.Add(loginCredentials);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearAllLoginCredentialsAsync()
        {
            _dbContext.LoginCredentials.RemoveRange(_dbContext.LoginCredentials);

            await _dbContext.SaveChangesAsync();
        }
    }
}
