using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Forms.UpdatePrompt
{
    public class UpdatesCheckResponse
    {
        public string DownloadUrl { get; set; }
        public bool IsNewVersionAvailable { get; set; }
    }
}
