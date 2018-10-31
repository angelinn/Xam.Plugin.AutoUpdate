using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;
using Xam.Plugin.AutoUpdate.Services;

namespace Xam.Plugin.AutoUpdate.UWP
{
    public class MicrosoftStoreOpener : IStoreOpener
    {
        public async void OpenStore()
        {
            await Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?ProductId={Package.Current.Id.FamilyName}"));
        }
    }
}
