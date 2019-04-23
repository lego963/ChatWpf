using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ChatWpf.Web.Server.Authentication
{
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        public AuthorizeTokenAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
