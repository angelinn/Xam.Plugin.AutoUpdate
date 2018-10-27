using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Xam.Plugin.AutoUpdate.Droid
{
    public static class AutoUpdate
    {
        public static Context Context { get; set; }
        public static string Authority { get; set; }

        public static void Init(Context activity, string fileProviderAuthority)
        {
            Context = activity;
            Authority = fileProviderAuthority;
        }
    }
}
