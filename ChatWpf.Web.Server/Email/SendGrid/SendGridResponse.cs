using System.Collections.Generic;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }
    }
}
