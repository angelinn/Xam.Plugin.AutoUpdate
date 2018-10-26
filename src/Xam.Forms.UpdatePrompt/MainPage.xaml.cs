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

        private void UpdatesCheck(object sender, UpdatesCheckArgs e)
        {
            e.UpdateTask = async () => { await Task.Delay(1000); return true; };
        }
    }
}
