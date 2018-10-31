# Xam.Plugins.AutoUpdate [In Development]

## Auto update for your Android/UWP

<div class="inline-block" >
  <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_android.png" alt="android" width="220"/>
  <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_uwp.png" alt="uwp" width="335"/>
</div>

TO DO: 
* Add docs how to poll from store
* Add option to open store instead of downloading app package

## What is it?
* Check for update and auto install sideloaded Android or UWP application
* Check for update and redirect to play store
* **The auto install part works only with UWP and Android**

## How does it work?
* Developer provides a check for updates function, returning if there is an update available and the url to the file, if provided
* The plugin checks for updates every ```RunEvery``` period of time
* When a new version is available and the user clicks the **confirm** button, the file from the provided url is downloaded and started

## Installation
Nuget package will be available soon.

Install the package only on the Forms project.

## Android
For Android API > **23** a ```FileProvider``` configuration is required:
* Add to AndroidManifest
```xml
  <application android:label="...">
    <provider android:name="android.support.v4.content.FileProvider" android:authorities="com.companyname.application" android:grantUriPermissions="true" android:exported="false">
      <meta-data android:name="android.support.FILE_PROVIDER_PATHS" android:resource="@xml/file_paths" />
    </provider>
  </application>
```

* Create a new file - ```Resources/xml/file_paths.xml```
```xml
<?xml version="1.0" encoding="utf-8"?>
<paths>
  <files-path name="files" path="/" />
</paths>
```

* Add to ```MainActivity```

```C#
AutoUpdate.Init(this, authority);

```

**NOTE:** The authority value is the same as the **android:authorities** in the ```AndroidManifest``` file.


## Usage

The ```UpdateManager``` class.

```C#
UpdateManagerParameters parameters = new UpdateManagerParameters
{
    Title = "Update available",
    Message = "A new version is available. Please update!",
    Confirm = "Update",
    Cancel = "Cancel",
    // choose how often to check when opening the app to avoid spamming the user every time
    RunEvery = TimeSpan.FromDays(1),
    CheckForUpdatesFunction = async () =>
    {
        // check for updates from external url ...
        return new UpdatesCheckResponse
        {
            IsNewVersionAvailable = true,
            DownloadUrl = "http://site.com/file.apk"
        };
    }
}
```

Use ```UpdateManagerMode.CheckAndAutoInstall``` to download and install the application
```C#
  UpdateManager updateManager = new UpdateManager(parameters, UpdateManagerMode.CheckAndAutoInstall);
```

or ```UpdateManagerMode.CheckAndOpenAppStore``` to open the corresponding app store
```C#
  UpdateManager updateManager = new UpdateManager(parameters, UpdateManagerMode.CheckAndOpenAppStore);
```
