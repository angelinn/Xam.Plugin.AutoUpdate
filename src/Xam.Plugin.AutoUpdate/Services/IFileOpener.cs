using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Plugin.AutoUpdate.Services
{
    public interface IFileOpener
    {
        void OpenFile(string path);
    }
}
