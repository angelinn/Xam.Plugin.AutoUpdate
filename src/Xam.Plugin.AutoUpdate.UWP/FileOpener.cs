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
            IStorageFolder directory = ApplicationData.Current.LocalFolder;
            foreach (IStorageFile file in await directory.GetFilesAsync())
            {
                if (file.FileType == ".appxbundle")
                    await file.DeleteAsync();
            }

            StorageFile sampleFile = await directory.CreateFileAsync(name);
            await FileIO.WriteBytesAsync(sampleFile, data);
            await Windows.System.Launcher.LaunchFileAsync(sampleFile);
        }
    }
}
