using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Forms.UpdatePrompt
{
    public class UpdatesCheckArgs
    {
        public bool IsNewVersionAvailable { get; set; }
        public Func<Task<bool>> UpdateTask { get; set; }
    }
}
   