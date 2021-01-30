using System;
using Microsoft.Win32;

namespace Nightlight
{
    class ThemeSwitcher
    {
        private bool _isLight;
        private bool _shouldToggleApps;
        private bool _shouldToggleSystem;

        /* Constants */
        private const String REG_KEY = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";
        private const String REG_VALUE_APPS = "AppsUseLightTheme";
        private const String REG_VALUE_SYSTEM = "SystemUsesLightTheme";

        public ThemeSwitcher()
        {
            // Get current value from apps registry - default to dark mode if not found 
            this._isLight = Convert.ToBoolean((Int32)Registry.GetValue(REG_KEY, REG_VALUE_APPS, 0));

            // By default, enable toggling of apps, disable toggling of system
            this._shouldToggleApps = true;
        }

        public bool getIsLight()
        {
            return this._isLight;
        }

        public void setShouldToggleApps(bool value)
        {
            this._shouldToggleApps = value;
        }

        public void setShouldToggleSystem(bool value)
        {
            this._shouldToggleSystem = value;
        }

        public void setThemeToLight()
        {
            if (this._shouldToggleApps)
            {
                Registry.SetValue(REG_KEY, REG_VALUE_APPS, 1, RegistryValueKind.DWord);
            }

            if (this._shouldToggleSystem)
            {
                Registry.SetValue(REG_KEY, REG_VALUE_SYSTEM, 1, RegistryValueKind.DWord);
            }

            this._isLight = true;
        }

        public void setThemeToDark()
        {
            if (this._shouldToggleApps)
            {
                Registry.SetValue(REG_KEY, REG_VALUE_APPS, 0, RegistryValueKind.DWord);
            }

            if (this._shouldToggleSystem)
            {
                Registry.SetValue(REG_KEY, REG_VALUE_SYSTEM, 0, RegistryValueKind.DWord);
            }

            this._isLight = false;
        }
    }
}