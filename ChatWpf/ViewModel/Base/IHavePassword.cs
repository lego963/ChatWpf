using System.Security;

namespace ChatWpf.ViewModel.Base
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}