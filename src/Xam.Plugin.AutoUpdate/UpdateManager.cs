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

        private readonly UpdateMode mode;

#if DEBUG
        public static string AppIDDummy
        {
            get
            {
                if (Device.RuntimePlatform == Device.Android)
                    return "com.spotify.music";
                if (Device.RuntimePlatform == Device.UWP)
                    return "9ncbcszsjrsb";
                if (Device.RuntimePlatform == Device.iOS)
                    return "id324684580";
                
                return String.Empty;
            }
        }
#endif

        private static UpdateManager instance;
        public static void Initialize(UpdateManagerParameters parameters, UpdateMode mode)
        {
            if (instance != null)
                throw new AutoUpdateException("UpdateManager is already initialized.");

            instance = new UpdateManager(parameters, mode);
        }

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

        private UpdateManager(UpdateManagerParameters parameters, UpdateMode mode)
            : this(parameters.Title, parameters.Message, parameters.Confirm, parameters.Cancel, parameters.CheckForUpdatesFunction, parameters.RunEvery)
        {
            if (mode == UpdateMode.MissingNo)
                throw new AutoUpdateException("You are not supposed to select this mode.");

            this.mode = mode;
        }

        private async void OnMainPageAppearing(object sender, EventArgs e)
        {
            if (!didCheck)
            {
                didCheck = true;

                bool run = true;
                if (runEvery.HasValue && Application.Current.Properties.TryGetValue("UpdateManager.LastUpdateTime", out object lastUpdate))
                {
                    DateTime lastUpdateTime = (DateTime)lastUpdate;
                    if (lastUpdateTime + runEvery.Value < DateTime.Now)
                        run = false;
                }

                if (run)
                    await CheckForUpdatesAsync();
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            if (mode == UpdateMode.AutoInstall)
                await CheckAndUpdateAsync();
            else if (mode == UpdateMode.OpenAppStore)
                await CheckAndOpenAppStoreAsync();

            Application.Current.Properties["UpdateManager.LastUpdateTime"] = DateTime.Now;
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
                DependencyService.Get<IStoreOpener>().OpenStore();
            }
        }

    }
}
