using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Plugin.CheckForUpdates
{
    public class UpdatesCheckResponse
    {
        public string DownloadUrl { get; set; }
        public bool IsNewVersionAvailable { get; set; }
    }
}
