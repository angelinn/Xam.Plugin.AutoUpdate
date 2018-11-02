# Xam.Plugin.AutoUpdate [In Development]

## Auto update for your Android/UWP

<div class="inline-block" >
  <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_android.png" alt="android" width="220"/>
  <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/install_android.png" alt="android" width="220"/>
  <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/update_uwp.jpg" alt="uwp" width="220"/>
    <img style="float: left;" src="https://github.com/angelinn/Xam.Plugin.UpdatePrompt/blob/master/images/install_uwp.jpg" alt="uwp" width="220"/>
</div>

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

Install the package on the mobile projects in your solution (.netstandard, Android, UWP, iOS).

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

* Create an ```UpdateManagerParameters``` option.
* Use ```UpdateManager.Initialize(parameters, mode)``` somewhere in your forms project. (e.g in **App.xaml.cs**)

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
        return new UpdatesCheckResponse(true, downloadUrl);
    }
}
```

Use ```UpdateMode.AutoInstall``` to download and install the application
```C#
UpdateManager.Initialize(parameters, UpdateMode.AutoInstall);
```

or ```UpdateMode.OpenAppStore``` to open the corresponding app store
```C#
UpdateManager.Initialize(parameters, UpdateMode.OpenAppStore);
```

## Auto install
Using the auto install mode, the plugin will download the file provided in the **DownloadUrl** parameter and launch it as **apk** or **appxbundle**, depending on the platform.

**Note:** As stated earlier, this option does not work with **iOS**, due to the restrictions of the operating system.

## Open app store
Using the open app store mode, the plugin will open the specified platform's **app store**, if an update is available.

**Note:** Additional logic is used for android to determine that only the **Google Play store** can open the ```market://``` url and no other app that has registered for it.
