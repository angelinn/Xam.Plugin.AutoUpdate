using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xam.Plugin.AutoUpdate.Services;

namespace Xam.Plugin.AutoUpdate.iOS
{
    public class AppStoreOpener : IStoreOpener
    {
        public void OpenStore()
        {
            NSString appID = NSBundle.MainBundle.InfoDictionary["CFBundleIdentifier"] as NSString;
            UIApplication.SharedApplication.OpenUrl(new NSUrl($"itms://itunes.apple.com/us/app/apple-store/{appID}?mt=8"));
        }
    }
}
