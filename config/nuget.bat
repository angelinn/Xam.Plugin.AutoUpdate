@echo off

mkdir bin
mkdir bin\netstandard1.4
mkdir bin\uap10
mkdir bin\MonoAndroid10

xcopy /Y ..\src\Xam.Plugin.AutoUpdate\bin\Debug\netstandard1.4\Xam.Plugin.AutoUpdate.dll bin\netstandard1.4\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.Droid\bin\Debug\Xam.Plugin.AutoUpdate.Droid.dll bin\MonoAndroid10\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.UWP\bin\Debug\Xam.Plugin.AutoUpdate.UWP.dll bin\UAP10\

