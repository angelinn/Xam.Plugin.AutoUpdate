using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xam.Plugin.AutoUpdate.Droid;
using Xam.Plugin.AutoUpdate.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlayStoreOpener))]
namespace Xam.Plugin.AutoUpdate.Droid
{
    public class PlayStoreOpener : IStoreOpener
    {
        public void OpenStore()
        {
            string appID =
#if DEBUG
                UpdateManager.AppIDDummy;
#else
                AutoUpdate.Context.PackageName;
#endif

            Intent storeIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse($"market://details?id={appID}"));
            bool foundApp = false;

            IList<ResolveInfo> otherApps = AutoUpdate.Context.PackageManager.QueryIntentActivities(storeIntent, PackageInfoFlags.Activities);
            foreach (ResolveInfo info in otherApps)
            {
                if (info.ActivityInfo.ApplicationInfo.PackageName == "com.android.vending")
                {
                    ActivityInfo storeActivityInfo = info.ActivityInfo;
                    ComponentName componentName = new ComponentName(storeActivityInfo.ApplicationInfo.PackageName, storeActivityInfo.Name);

                    storeIntent.AddFlags(ActivityFlags.NewTask);
                    storeIntent.AddFlags(ActivityFlags.ResetTaskIfNeeded);
                    storeIntent.AddFlags(ActivityFlags.ClearTop);

                    storeIntent.SetComponent(componentName);
                    AutoUpdate.Context.StartActivity(storeIntent);

                    foundApp = true;
                    break;
                }
            }

            if (!foundApp)
                throw new Exception("Could not find google play store app.");
        }
    }
}
