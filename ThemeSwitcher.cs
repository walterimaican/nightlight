using System;
using Microsoft.Win32;

namespace Nightlight
{
    class ThemeSwitcher
    {
        // TODO
        // Offer SystemUsesLightTheme as well (flips taskbar and startmenu)
        // AppsUseLightTheme is for everything else
        private bool _isLight;

        /* Constants */
        private const String REG_KEY = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";
        private const String REG_VALUE = "AppsUseLightTheme";

        public ThemeSwitcher()
        {
            _isLight = Convert.ToBoolean((Int32)Registry.GetValue(REG_KEY, REG_VALUE, 1));
        }

        public bool getIsLight()
        {
            return this._isLight;
        }

        public void setThemeToLight()
        {
            Registry.SetValue(REG_KEY, REG_VALUE, 1, RegistryValueKind.DWord);
            this._isLight = true;
        }

        public void setThemeToDark()
        {
            Registry.SetValue(REG_KEY, REG_VALUE, 0, RegistryValueKind.DWord);
            this._isLight = false;
        }
    }
}