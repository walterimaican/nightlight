# Nightlight

A simple and lightweight notification icon to toggle Windows theming between light mode and dark mode.

## Table of Contents
1. [Installing](#installing)
2. [Using Nightlight](#using-nightlight)
    - [General Usage](#general-usage)
    - [Settings](#settings)
    - [Suggestions](#suggestions)
3. [Compatibility](#compatibility)
4. [Uninstalling](#uninstalling)
5. [For Developers](#for-developers)
    - [Prerequisites](#prerequisites)
    - [Development](#development)
    - [Deployment](#deployment)
6. [Credits](#credits)

# Installing

Go to the latest release here:

![releases.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/releases.png)

Download the release-x.x.x.x.tar:

![download-release.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/download-release.png)

Extract the release (any program works - you can download [WinRar here](https://www.win-rar.com/predownload.html?&L=0) or [7-Zip here](https://www.7-zip.org/)):

![extract.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/extract.png)

![extracted-release.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/extracted-release.png)

Once inside the extracted release folder, double click on the `RUN_ME.bat` and install:

![run_me.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/run_me.png)

![install.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/install.png)

The installer may redirect you to install the .NET runtime - you will need to install it (developers: if you install the .NET SDK, the runtime comes bundled):

![install-dot-net.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/install-dot-net.png)

![dot-net-64-86.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/dot-net-64-86.png)

![dot-net-exe.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/dot-net-exe.png)

If you had to install the runtime, once it is installed, double click on the `RUN_ME.bat` and complete installation.

And that's it! Nightlight should now be installed on your computer:

![installed.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/installed.png)

If you don't immediately see it in your system/notification tray, check to see if it is hidden:

![hidden.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/hidden.png)

You can now delete the release files (Nightlight has been installed and does not rely on these anymore):

![delete.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/delete.png)

# Using Nightlight

## General Usage

Left click on the Nightlight icon to toggle between light and dark mode.

Right click on the Nightlight icon to bring up the context menu. 

![contextMenu.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/contextMenu.png)

## Settings

By default, only the `"Apps, Explorer, System"` option is enabled. When you toggle between light and dark, this will toggle Windows Applications, Windows File Explorer, as well as other applications that are listening to the "system's theme". See [Suggestions](#suggestions) for more details!

![light.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/light.png)

![dark.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/dark.png)

You can also enable the `"Start Menu & Taskbar"` option. When enabled, toggling between light and dark will also toggle the Start Menu and the Taskbar.

![lightSystem.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/lightSystem.png)

![darkSystem.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/darkSystem.png)

## Suggestions

While Nightlight can only toggle Windows theming, you can tie in various applications with it! (You must have the "Apps, Explorer, System" option checked).

Below are some applications that can toggle with your OS, given you install the accompanying extensions:

- VS Code: [Toggle Light/Dark Theme](https://marketplace.visualstudio.com/items?itemName=danielgjackson.auto-dark-mode-windows)
- Google Chrome: [Dark Reader](https://chrome.google.com/webstore/detail/dark-reader/eimadpbcbfnmbkopoojfekhnkhdbieeh)
- GitHub (web page):

![github-dark-mode.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/github-dark-mode.png)

# Compatibility

Nightlight should work on any Windows 10 build after version 1607 (released December 16, 2015), with varying degrees of success. As updates were rolled out, more applications gained dark mode support.

If you'd like to verify that Nightlight will work on your system, perform the following steps:

## 1

Open `Windows Run` by doing either of the following:
- search for "Run" in your start menu
- press the "Windows" and "R" key on your keyboard at the same time

Type into the search box:

```
regedit
```
and search.

![run.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/run.png)

## 2

After opening the Registry Editor, go to the following location:

```
Computer\HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize
```

If you see `AppsUseLightTheme` and `SystemUsesLightTheme` then Nightlight is compatible!

![registry.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/registry.png)

# Uninstalling

To uninstall, go to `Apps & Features`, by doing either of the following:
- Right-click on the Windows Icon (Start Menu Button) and click `Apps and Features`
- Press the "Windows" and "X" keys at the same time, followed by the "F" key

![apps-and-features.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/apps-and-features.png)

From there, look for Nightlight and click the Uninstall button

![uninstall.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/uninstall.png)

![uninstall-clickonce.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/uninstall-clickonce.png)

If you had Nightlight running, you may have to exit out of separately as uninstalling (currently) does not stop the program.

# For Developers

## Prerequisites

While Nightlight was built on a Windows machine for Windows installs, a few of the Makefile recipes use UNIX commands. In order to use these recipes, I recommend installing [Cmder](https://cmder.net/).

Nightlight was created with .Net 5.0.2 and written in C#.  Install the latest version of .Net from [here](https://dotnet.microsoft.com/download/dotnet/).

## Development


To do a clean build and start Nightlight, run the following command in your CLI:

```
make
```

- `Program.cs` is the main entry point to Nightlight
- `NightlightApp.cs` contains the icon UI
- `ThemeSwitcher.cs` contains the code to change Windows Registry values
- `assets` contains the icon files

## Deployment

Deployment is done via [ClickOnce](https://devblogs.microsoft.com/dotnet/announcing-net-5-0-rc-2/#clickonce).

The following allows you to deploy via `ClickOnce` via a CLI:

Download `Mage`:

```
dotnet tool install --global Microsoft.DotNet.Mage
```

Update the version in `nightlight.csproj`:

![versioning.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/versioning.png)

And then run the following:

```
make release
```

The resulting `release-x.x.x.x.tar` is the distributable that will be downloaded by end users.

# Credits

Settings icon was made by Freepik from www.flaticon.com.

All other icons were custom-made for this program.