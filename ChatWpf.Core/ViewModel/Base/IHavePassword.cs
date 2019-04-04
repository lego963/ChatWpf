using System.Security;

namespace ChatWpf.Core.ViewModel.Base
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
