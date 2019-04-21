using ChatWpf.Core.ApiModels;
using ChatWpf.ViewModel.Dialogs;
using Dna;
using static ChatWpf.DI.DI;

namespace ChatWpf.WebRequests
{
    public static class WebRequestResultExtensions
    {
        public static async System.Threading.Tasks.Task<bool> DisplayErrorIfFailedAsync<T>(this WebRequestResult<ApiResponse<T>> response, string title)
        {
            // If there was no response, bad data, or a response with a error message...
            if (response == null || response.ServerResponse == null || !response.ServerResponse.Successful)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unknown error from server call";

                // If we got a response from the server...
                if (response?.ServerResponse != null)
                    // Set message to servers response
                    message = response.ServerResponse.ErrorMessage;
                // If we have a result but deserialize failed...
                else if (!string.IsNullOrWhiteSpace(response?.RawServerResponse))
                    // Set error message
                    message = $"Unexpected response from server. {response.RawServerResponse}";
                // If we have a result but no server response details at all...
                else if (response != null)
                    // Set message to standard HTTP server response details
                    message = $"Failed to communicate with server. Status code {response.StatusCode}. {response.StatusDescription}";

                // Display error
                await UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    // TODO: Localize strings
                    Title = title,
                    Message = message
                });

                // Return that we had an error
                return true;
            }

            // All was OK, so return false for no error
            return false;
        }
    }
}
