using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public class SendGridResponse
    {
        /// <summary>
        /// Any errors from a response
        /// </summary>
        public List<SendGridResponseError> Errors { get; set; }
    }
}
