using Microsoft.Win32;

namespace Nightlight
{
    class ThemeSwitcher
    {
        // SET REG_KEY=HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize
        // SET REG_VALUE=AppsUseLightTheme
        // FOR /F "tokens=2*" %%A IN ('REG QUERY %REG_KEY% /v %REG_VALUE%') DO SET IS_LIGHT_NOT_DARK=%%B
        // IF %IS_LIGHT_NOT_DARK%==0x1 (REG ADD %REG_KEY% /v %REG_VALUE% /t REG_DWORD /d 0 /f) ELSE (REG ADD %REG_KEY% /v %REG_VALUE% /t REG_DWORD /d 1 /f)

        private bool _isLight;

        public ThemeSwitcher()
        {
            // TODO
            _isLight = false;
        }

        public bool getIsLight()
        {
            return this._isLight;
        }

        public void setThemeToLight()
        {
            this._isLight = true;
        }

        public void setThemeToDark()
        {
            this._isLight = false;
        }
    }
}