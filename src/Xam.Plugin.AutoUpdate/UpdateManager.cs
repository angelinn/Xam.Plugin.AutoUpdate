using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin.AutoUpdate.Exceptions;
using Xam.Plugin.AutoUpdate.Services;
using Xamarin.Forms;

namespace Xam.Plugin.AutoUpdate
{
    public class UpdateManager
    {
        private readonly string title;
        private readonly string message;
        private readonly string confirm;
        private readonly string cancel;
        private readonly Func<Task<UpdatesCheckResponse>> checkForUpdatesFunction;
        private readonly TimeSpan? runEvery;

        private bool didCheck;
        private readonly Page mainPage;

        private readonly UpdateManagerMode mode;

#if DEBUG
        public static string AppIDDummy
        {
            get
            {
                if (Device.RuntimePlatform == Device.Android)
                    return "com.spotify.music";
                if (Device.RuntimePlatform == Device.UWP)
                    return "spotify-music";
                if (Device.RuntimePlatform == Device.iOS)
                    return "spotify-music";

                return String.Empty;
            }
        }
#endif

        private UpdateManager(string title, string message, string confirm, string cancel, Func<Task<UpdatesCheckResponse>> checkForUpdatesFunction, TimeSpan? runEvery = null)
        {
            this.title = title;
            this.message = message;
            this.confirm = confirm;
            this.cancel = cancel;
            this.runEvery = runEvery;
            this.checkForUpdatesFunction = checkForUpdatesFunction ?? throw new AutoUpdateException("Check for updates function not provided. You must pass it in the constructor.");

            mainPage = Application.Current.MainPage;
            mainPage.Appearing += OnMainPageAppearing;
        }
        
        public UpdateManager(UpdateManagerParameters parameters, UpdateManagerMode mode)
            : this(parameters.Title, parameters.Message, parameters.Confirm, parameters.Cancel, parameters.CheckForUpdatesFunction, parameters.RunEvery)
        {
            if (mode == UpdateManagerMode.MissingNo)
                throw new AutoUpdateException("You are not supposed to select this mode.");

            this.mode = mode;
        }

        private async void OnMainPageAppearing(object sender, EventArgs e)
        {
            if (!didCheck)
            {
                didCheck = true;

                if (runEvery.HasValue)
                {
                    DateTime lastUpdateTime = (DateTime)Application.Current.Properties["UpdateManager.LastUpdateTime"];
                    if (lastUpdateTime + runEvery.Value > DateTime.Now)
                    {
                        Application.Current.Properties["UpdateManager.LastUpdateTime"] = DateTime.Now;
                        await CheckForUpdatesAsync();
                    }
                }
                else
                    await CheckForUpdatesAsync();
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            if (mode == UpdateManagerMode.CheckAndAutoInstall)
                await CheckAndUpdateAsync();
            else if (mode == UpdateManagerMode.CheckAndOpenAppStore)
                await CheckAndOpenAppStoreAsync();
        }

        private async Task CheckAndUpdateAsync()
        {
            UpdatesCheckResponse response = await checkForUpdatesFunction();
            if (response.IsNewVersionAvailable && await mainPage.DisplayAlert(title, message, confirm, cancel))
            {
                if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.Android)
                {
                    HttpResponseMessage httpResponse = await new HttpClient().GetAsync(response.DownloadUrl);
                    byte[] data = await httpResponse.Content.ReadAsByteArrayAsync();

                    string fileName = response.DownloadUrl.Substring(response.DownloadUrl.LastIndexOf("/") + 1);
                    DependencyService.Get<IFileOpener>().OpenFile(data, fileName);
                }
                else
                    throw new AutoUpdateException("Only Android and UWP are supported for automatic installation.");
            }
        }

        private async Task CheckAndOpenAppStoreAsync()
        {
            UpdatesCheckResponse response = await checkForUpdatesFunction();
            if (response.IsNewVersionAvailable && await mainPage.DisplayAlert(title, message, confirm, cancel))
            {
                // open app store url
            }
        }

    }
}
