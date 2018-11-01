using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Plugin.AutoUpdate
{
    public class UpdatesCheckResponse
    {
        public string DownloadUrl { get; set; }
        public bool IsNewVersionAvailable { get; set; }

        public UpdatesCheckResponse(bool isNewVersionAvailable, string downloadUrl = null)
        {
            IsNewVersionAvailable = isNewVersionAvailable;
            DownloadUrl = downloadUrl;
        }
    }
}
