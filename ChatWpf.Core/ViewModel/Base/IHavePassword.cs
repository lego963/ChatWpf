using System.Security;

namespace ChatWpf.Core
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}