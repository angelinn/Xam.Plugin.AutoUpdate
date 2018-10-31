using System;
using System.Threading.Tasks;
using Xam.Plugin.AutoUpdate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Samples
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new MainPage();
            string downloadUrl = String.Empty;
            if (Device.RuntimePlatform == Device.Android)
                downloadUrl = "https://github.com/angelinn/TramlineFive.Xamarin/releases/download/2.8/com.TramlineFive.beta.v2.8.apk";
            else if (Device.RuntimePlatform == Device.UWP)
                downloadUrl = "https://github.com/angelinn/TramlineFive.Xamarin/releases/download/2.8/TramlineFive.UWP_2.8.0.0_arm.appxbundle";

            UpdateManagerParameters parameters = new UpdateManagerParameters
            {
                Title = "Update available",
                Message = "A new version is available. Please update!",
                Confirm = "Update",
                Cancel = "Cancel",
                // choose how often to check when opening the app to avoid spamming the user every time
                RunEvery = TimeSpan.FromDays(1),
                CheckForUpdatesFunction = async () =>
                {
                    // check for updates from external url ...
                    return new UpdatesCheckResponse
                    {
                        IsNewVersionAvailable = true,
                        DownloadUrl = downloadUrl
                    };
                }
            };

            UpdateManager updateManager = new UpdateManager(parameters, UpdateManagerMode.CheckAndAutoInstall);
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
