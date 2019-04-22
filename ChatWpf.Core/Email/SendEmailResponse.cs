using System.Collections.Generic;

namespace ChatWpf.Core.Email
{
    public class SendEmailResponse
    {
        public bool Successful => !(Errors?.Count > 0);

        public List<string> Errors { get; set; }
    }
}
