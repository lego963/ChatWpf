using System.Threading.Tasks;
using ChatWpf.Core.ApiModels;
using ChatWpf.ViewModel.Dialogs;
using Dna;

namespace ChatWpf.WebRequests
{
    public static class WebRequestResultExtensions
    {
        public static async Task<bool> HandleErrorIfFailedAsync(this WebRequestResult response, string title)
        {
            if (response == null || response.ServerResponse == null || (response.ServerResponse as ApiResponse)?.Successful == false)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unknown error from server call";

                if (response?.ServerResponse is ApiResponse apiResponse)
                    message = apiResponse.ErrorMessage;
                else if (!string.IsNullOrWhiteSpace(response?.RawServerResponse))
                    message = $"Unexpected response from server. {response.RawServerResponse}";
                else if (response != null)
                    message = response.ErrorMessage ?? $"Server responded with {response.StatusDescription} ({response.StatusCode})";

                if (response?.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    FrameworkDI.Logger.LogInformationSource("Logging user out due to unauthorized response from server");

                    await DI.Di.ViewModelSettings.LogoutAsync();
                }
                else
                {
                    await DI.Di.Ui.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = title,
                        Message = message
                    });
                }

                return true;
            }

            return false;
        }
    }
}
