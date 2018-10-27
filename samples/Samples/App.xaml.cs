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
            UpdateManager updateManager = new UpdateManager(
                title: "Update available",
                message: "A new version is available. Please update!",
                confirm: "Update",
                cancel: "Cancel",
                checkForUpdatesFunction: async () =>
                {
                    await Task.Delay(3000);
                    return new UpdatesCheckResponse
                    {
                        IsNewVersionAvailable = true,
                        DownloadUrl = "https://github.com/angelinn/TramlineFive.Xamarin/releases/download/2.8/TramlineFive.UWP_2.8.0.0_arm.appxbundle"
                        //DownloadUrl = "https://github.com/angelinn/TramlineFive.Xamarin/releases/download/2.8/com.TramlineFive.beta.v2.8.apk"
                    };
                }
            );
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
