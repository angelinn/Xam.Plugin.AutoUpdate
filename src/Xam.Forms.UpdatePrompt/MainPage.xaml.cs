using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xam.Forms.UpdatePrompt
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            bool result = await DisplayAlert("New version", "An update of the application is available.", "Download", "Cancel");
            if (result)
                Device.OpenUri(new Uri("http://google.bg"));
        }

        private void UpdatesCheck(object sender, UpdatesCheckArgs e)
        {
            e.UpdateTask = async () =>
            {
                await Task.Delay(1000);
                return new UpdatesCheckResponse
                {
                    IsNewVersionAvailable = true,
                    DownloadUrl = "http://google.bg"
                };
            };
        }
    }
}
