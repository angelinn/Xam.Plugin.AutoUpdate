@echo off

mkdir bin
mkdir bin\netstandard1.4
mkdir bin\netstandard2.0
mkdir bin\uap10.0
mkdir bin\monoandroid81

xcopy /Y ..\src\Xam.Plugin.AutoUpdate\bin\Debug\netstandard1.4\Xam.Plugin.AutoUpdate.dll bin\netstandard1.4\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate\bin\Debug\netstandard1.4\Xam.Plugin.AutoUpdate.dll bin\netstandard2.0\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.Droid\bin\Debug\Xam.Plugin.AutoUpdate.Droid.dll bin\monoandroid81\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.Droid\bin\Debug\Xam.Plugin.AutoUpdate.dll bin\monoandroid81\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.UWP\bin\Debug\Xam.Plugin.AutoUpdate.UWP.dll bin\UAP10.0\
xcopy /Y ..\src\Xam.Plugin.AutoUpdate.UWP\bin\Debug\Xam.Plugin.AutoUpdate.dll bin\UAP10.0\
