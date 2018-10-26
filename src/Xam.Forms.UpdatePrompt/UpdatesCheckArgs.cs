using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Forms.UpdatePrompt
{
    public class UpdatesCheckArgs
    {
        public Func<Task<UpdatesCheckResponse>> UpdateTask { get; set; }
    }

    public class UpdatesCheckResponse
    {
        public bool IsNewVersionAvailable { get; set; }
        public string DownloadUrl { get; set; }
    }
}
