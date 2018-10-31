using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Plugin.AutoUpdate
{
    public class UpdateManagerParameters
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Confirm { get; set; }
        public string Cancel { get; set; }
        public Func<Task<UpdatesCheckResponse>> CheckForUpdatesFunction { get; set; }
        public TimeSpan? RunEvery { get; set; }
    }
}
