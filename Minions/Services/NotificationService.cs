using Microsoft.JSInterop;

namespace Minions.Services
{

    public class NotificationService
    {
        private readonly IJSRuntime _jSRuntime;

        public NotificationService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task Alert(string message)
        {
            await _jSRuntime.InvokeVoidAsync("alert", message);
        }

        public async Task<string> Prompt(string message)
        {
            return await _jSRuntime.InvokeAsync<string>("prompt", message);
        }

        public async Task<bool> Confirm(string message)
        {
            return await _jSRuntime.InvokeAsync<bool>("confirm", message);
        }

    }
}
