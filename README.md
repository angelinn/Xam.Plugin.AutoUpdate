# Xam.Plugin.AutoUpdate
## Auto update for your android/UWP

<div class="inline-block">
  <img src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_android.png" alt="android" width="220"/>
</div>
<div class="inline-block" style="margin-left: 100px">
  <img src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_uwp.png" alt="uwp" width="335"/>
</div>


## What is it?
* Downloads and installs a new version of your application
* Uses native alert view

## Installation
Nuget package will be available soon.

Install the package only on the Forms project.

## Android
For android api > 23 a FileProvider configuration is required:
* Add to AndroidManifest
```xml
  <application android:label="...">
    <provider android:name="android.support.v4.content.FileProvider" android:authorities="com.companyname.application" android:grantUriPermissions="true" android:exported="false">
      <meta-data android:name="android.support.FILE_PROVIDER_PATHS" android:resource="@xml/file_paths" />
    </provider>
  </application>
```

* Create a new file - Resources/xml/file_paths.xml
```xml
<?xml version="1.0" encoding="utf-8"?>
<paths>
  <files-path name="files" path="/" />
</paths>
```

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
            DownloadUrl = "http://site.com/file.apk"
        };
    }
);

```
