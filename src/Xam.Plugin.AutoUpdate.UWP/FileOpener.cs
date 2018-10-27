using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Xam.Plugin.AutoUpdate.Services;
using Xam.Plugin.AutoUpdate.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileOpener))]
namespace Xam.Plugin.AutoUpdate.UWP
{
    public class FileOpener : IFileOpener
    {
        public async void OpenFile(byte[] data, string name)
        {
            string directory = ApplicationData.Current.LocalFolder.Path;
            foreach (string file in Directory.GetFiles(directory))
            {
                if (Path.GetExtension(file) == "*.appxbundle")
                    File.Delete(file);
            }

            StorageFile sampleFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(name);
            await FileIO.WriteBytesAsync(sampleFile, data);
            await Windows.System.Launcher.LaunchFileAsync(sampleFile);
        }
    }
}
