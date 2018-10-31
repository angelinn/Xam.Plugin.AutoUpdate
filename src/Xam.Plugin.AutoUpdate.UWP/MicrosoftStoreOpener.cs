using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;
using Xam.Plugin.AutoUpdate.Services;
using Xam.Plugin.AutoUpdate.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(MicrosoftStoreOpener))]
namespace Xam.Plugin.AutoUpdate.UWP
{
    public class MicrosoftStoreOpener : IStoreOpener
    {
        public async void OpenStore()
        {
            string appID =
#if DEBUG
                UpdateManager.AppIDDummy;
#else
                Package.Current.Id.FamilyName;
#endif
            await Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?ProductId={appID}"));
        }
    }
}
