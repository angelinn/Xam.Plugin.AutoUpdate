# Xam.Plugin.CheckForUpdates

<div class="inline-block">
  <img src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_android.png" alt="android" width="220"/>
</div>
<div class="inline-block" style="margin-left: 100px">
  <img src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_uwp.png" alt="uwp" width="335"/>
</div>


## What is it?
* Uses native alert view
* On confirm button pressed, opens the default browser to the download page

## Installation
Nuget package will be available soon.

Install the package only on the Forms project.

## Usage

The ```UpdateManager``` class.

```C#
UpdateManager updateManager = new UpdateManager(
    title: "Update available",
    message: "A new version is available. Please update!",
    confirm: "Update",
    cancel: "Cancel",
    
    // choose how often to check when opening the app to avoid spamming the user every time
    runEvery: TimeSpan.FromDays(1), 
    checkForUpdatesFunction: async () =>
    {
        // check for updates from external url ...
        return new UpdatesCheckResponse
        {
            IsNewVersionAvailable = true,
            DownloadUrl = "http://github.com"
        };
    }
);

```
