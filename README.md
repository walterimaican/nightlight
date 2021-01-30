# Nightlight

A system tray/notification icon to quickly change Windows theming between light and dark.

## Table of Contents
- [Installing](#installing)
- [Using Nightlight](#using-nightlight)
- [Compatibility](#compatibility)
- [For Developers](#for-developers)
- [Credits](#credits)

# Installing

TODO

TODO -> Add capability to start Nightlight on Windows startup

# Using Nightlight

Left click on the Nightlight icon to toggle between light and dark mode.

Right click on the Nightlight icon to bring up the context menu. 

![contextMenu.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/contextMenu.png)

## Settings

By default, only the `"Apps, Explorer, System"` option is enabled. When you toggle between light and dark, this will toggle Windows Applications, Windows File Explorer, as well as other applications that are listening to the "system's theme", such as Google Chrome via the [Dark Reader](https://chrome.google.com/webstore/detail/dark-reader/eimadpbcbfnmbkopoojfekhnkhdbieeh) extension.

![light.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/light.png)

![dark.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/dark.png)

You can also enable the `"Start Menu & Taskbar"` option. When enabled, toggling between light and dark will also toggle the Start Menu and the Taskbar.

![lightSystem.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/lightSystem.png)

![darkSystem.png](https://github.com/walterimaican/nightlight/blob/main/readme-images/darkSystem.png)

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

# For Developers

Nightlight was created with .Net 5.0.2 and written in C#.  Install the latest version of .Net from [here](https://dotnet.microsoft.com/download/dotnet/)

- `Program.cs` is the main entry point to Nightlight
- `NightlightApp.cs` contains the icon UI
- `ThemeSwitcher.cs` contains the code to change Windows Registry values

To do a clean build and run Nightlight, use the following command in your CLI:
```
make
```

# Credits

Settings icon was made by Freepik from www.flaticon.com.

All other icons were custom-made for this program.