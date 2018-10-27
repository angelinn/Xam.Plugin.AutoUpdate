using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Plugin.AutoUpdate
{
    public class UpdatesCheckResponse
    {
        public string DownloadUrl { get; set; }
        public bool IsNewVersionAvailable { get; set; }
    }
}
