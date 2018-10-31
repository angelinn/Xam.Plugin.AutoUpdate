using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xam.Plugin.AutoUpdate.iOS;
using Xam.Plugin.AutoUpdate.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppStoreOpener))]
namespace Xam.Plugin.AutoUpdate.iOS
{
    public class AppStoreOpener : IStoreOpener
    {
        public void OpenStore()
        {
            string appID =
#if DEBUG
                UpdateManager.AppIDDummy;
#else
                NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"] as NSString;
#endif
            
            UIApplication.SharedApplication.OpenUrl(new NSUrl($"itms://itunes.apple.com/us/app/apple-store/{appID}?mt=8"));
        }
    }
}
