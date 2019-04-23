using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChatWpf.Core.DI.Interfaces;
using ChatWpf.Core.Email;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using static Dna.FrameworkDI;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            var apiKey = Configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(details.FromEmail, details.FromName);
            var to = new EmailAddress(details.ToEmail, details.ToName);
            var subject = details.Subject;
            var content = details.Content;

            var msg = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                details.IsHtml ? null : details.Content,
                details.IsHtml ? details.Content : null);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == HttpStatusCode.Accepted)
                return new SendEmailResponse();

            try
            {
                var bodyResult = await response.Body.ReadAsStringAsync();
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);
                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };
                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)
                    // Add an unknown error
                    // TODO: Localization
                    errorResponse.Errors = new List<string>(new[] { "Unknown error from email sending service. Please contact Synthesis support." });

                return errorResponse;

            }
            catch (Exception ex)
            {
                // TODO: Localization
                // Break if we are debugging
                if (Debugger.IsAttached)
                {
                    var error = ex;
                    Debugger.Break();
                }

                return new SendEmailResponse
                {
                    Errors = new List<string>(new[] { "Unknown error occurred" })
                };
            }
        }
    }
}
