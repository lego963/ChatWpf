using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public class SendGridResponseError
    {
        /// <summary>
        /// The error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The field inside the email message details that the error is related to
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Useful information for resolving the error
        /// </summary>
        public string Help { get; set; }
    }
}
