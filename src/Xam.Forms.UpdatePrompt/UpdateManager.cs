using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xam.Forms.UpdatePrompt
{
    public class UpdateManager
    {
        private readonly string title;
        private readonly string message;
        private readonly string confirm;
        private readonly string cancel;
        private readonly Func<Task<UpdatesCheckResponse>> checkForUpdatesFunction;

        private bool didCheck;
        
        public UpdateManager(string title, string message, string confirm, string cancel, Func<Task<UpdatesCheckResponse>> checkForUpdatesFunction)
        {
            this.title = title;
            this.message = message;
            this.confirm = confirm;
            this.cancel = cancel;
            this.checkForUpdatesFunction = checkForUpdatesFunction;

            App.Current.MainPage.Appearing += OnMainPageAppearing;
        }

        private async void OnMainPageAppearing(object sender, EventArgs e)
        {
            if (!didCheck)
            {
                didCheck = true;
                await CheckForUpdatesAsync();
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            UpdatesCheckResponse response = await checkForUpdatesFunction();
            if (response.IsNewVersionAvailable)
            {
                if (await App.Current.MainPage.DisplayAlert(title, message, confirm, cancel))
                    Device.OpenUri(new Uri(response.DownloadUrl));
            }
        }
    }
}
