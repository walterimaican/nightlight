using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Nightlight
{
    class NightlightApp : ApplicationContext
    {
        private ThemeSwitcher _themeSwitcher;
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private Icon _lightIcon;
        private Icon _darkIcon;

        /* Constants */
        private const String LIGHT_ICON_PATH = "assets/light.ico";
        private const String DARK_ICON_PATH = "assets/dark.ico";
        private const String SETTINGS_ICON_PATH = "assets/settings.ico";
        private const String HELP_ICON_PATH = "assets/help.ico";
        private const String ICON_LIGHT_TEXT = "Nightlight: Light";
        private const String ICON_DARK_TEXT = "Nightlight: Dark";
        private Color LIGHT_MODE_BACKGROUND = Color.WhiteSmoke;
        private Color DARK_MODE_BACKGROUND = Color.FromArgb(50, 50, 50);
        private Color LIGHT_MODE_TEXT = Color.Black;
        private Color DARK_MODE_TEXT = Color.WhiteSmoke;

        public NightlightApp()
        {
            _themeSwitcher = new ThemeSwitcher();

            // Icons
            _lightIcon = new Icon(LIGHT_ICON_PATH);
            _darkIcon = new Icon(DARK_ICON_PATH);

            // Context Menu Items
            ToolStripMenuItem lightModeButton = new ToolStripMenuItem();
            lightModeButton.Image = Image.FromFile(LIGHT_ICON_PATH);
            lightModeButton.Text = "Activate Light Mode";
            lightModeButton.Click += OnLightMode;

            ToolStripMenuItem darkModeButton = new ToolStripMenuItem();
            darkModeButton.Image = Image.FromFile(DARK_ICON_PATH);
            darkModeButton.Text = "Activate Dark Mode";
            darkModeButton.Click += OnDarkMode;

            // Misnomer: The affected registry is "AppsUseLightTheme",
            // however in effect, this is what flips the whole system.
            // By default, this option is enabled.
            ToolStripMenuItem appsThemeButton = new ToolStripMenuItem();
            appsThemeButton.Text = "Apps, Explorer, System";
            appsThemeButton.CheckOnClick = true;
            appsThemeButton.Click += OnAppsToggle;
            appsThemeButton.Checked = true;

            // Although the affected registry is "SystemUsesLightTheme",
            // this only flips Start Menu and Taskbar
            // By default, this option is NOT enabled.
            ToolStripMenuItem systemThemeButton = new ToolStripMenuItem();
            systemThemeButton.Text = "Start Menu && Taskbar";
            systemThemeButton.CheckOnClick = true;
            systemThemeButton.Click += OnSystemToggle;

            ToolStripMenuItem settingsButton = new ToolStripMenuItem();
            settingsButton.Image = Image.FromFile(SETTINGS_ICON_PATH);
            settingsButton.Text = "Settings";
            settingsButton.DropDownItems.Add(appsThemeButton);
            settingsButton.DropDownItems.Add(systemThemeButton);

            ToolStripMenuItem aboutButton = new ToolStripMenuItem();
            aboutButton.Image = Image.FromFile(HELP_ICON_PATH);
            aboutButton.Text = "Help && About";
            aboutButton.Click += OnHelpAndAbout;

            ToolStripMenuItem exitButton = new ToolStripMenuItem();
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;

            ToolStripMenuItem[] toolStripMenuItems = new ToolStripMenuItem[] {
                lightModeButton,
                darkModeButton,
                settingsButton,
                aboutButton,
                exitButton
            };

            // Context Menu
            _contextMenuStrip = new ContextMenuStrip();
            _contextMenuStrip.ShowItemToolTips = false;
            _contextMenuStrip.Items.AddRange(toolStripMenuItems);
            _contextMenuStrip.Renderer = new CustomRenderer();

            // Notify Icon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = _contextMenuStrip;
            _notifyIcon.Click += OnIconClick;

            // Initialize UI
            if (_themeSwitcher.getIsLight())
            {
                activateLightMode();
            }
            else
            {
                activateDarkMode();
            }
        }

        private void activateLightMode()
        {
            foreach (ToolStripMenuItem item in _contextMenuStrip.Items)
            {
                item.BackColor = LIGHT_MODE_BACKGROUND;
                item.ForeColor = LIGHT_MODE_TEXT;

                if (item.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    {
                        subItem.BackColor = LIGHT_MODE_BACKGROUND;
                        subItem.ForeColor = LIGHT_MODE_TEXT;
                    }
                }
            }
            _notifyIcon.Icon = _lightIcon;
            _notifyIcon.Text = ICON_LIGHT_TEXT;

            _themeSwitcher.setThemeToLight();
        }

        private void activateDarkMode()
        {
            foreach (ToolStripMenuItem item in _contextMenuStrip.Items)
            {
                item.BackColor = DARK_MODE_BACKGROUND;
                item.ForeColor = DARK_MODE_TEXT;

                if (item.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in item.DropDownItems)
                    {
                        subItem.BackColor = DARK_MODE_BACKGROUND;
                        subItem.ForeColor = DARK_MODE_TEXT;
                    }
                }
            }
            _notifyIcon.Icon = _darkIcon;
            _notifyIcon.Text = ICON_DARK_TEXT;

            _themeSwitcher.setThemeToDark();
        }

        void OnIconClick(object sender, EventArgs e)
        {
            // If icon is right-clicked, user is opening context menu, do not toggle Nightlight
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                return;
            }

            // Toggle Nightlight
            if (_themeSwitcher.getIsLight())
            {
                activateDarkMode();
            }
            else
            {
                activateLightMode();
            }
        }

        void OnLightMode(object sender, EventArgs e)
        {
            activateLightMode();
        }

        void OnDarkMode(object sender, EventArgs e)
        {
            activateDarkMode();
        }

        void OnAppsToggle(object sender, EventArgs e)
        {
            ToolStripMenuItem appsButton = sender as ToolStripMenuItem;
            _themeSwitcher.setShouldToggleApps(appsButton.Checked);
        }

        void OnSystemToggle(object sender, EventArgs e)
        {
            ToolStripMenuItem systemButton = sender as ToolStripMenuItem;
            _themeSwitcher.setShouldToggleSystem(systemButton.Checked);

        }

        void OnHelpAndAbout(object sender, EventArgs e)
        {
            String url = "https://github.com/walterimaican/nightlight";

            // See https://github.com/dotnet/corefx/issues/10361
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }

        void OnExit(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            Application.Exit();
        }
        class CustomRenderer : ToolStripProfessionalRenderer
        {
            public CustomRenderer() : base(new CustomColors()) { }
        }

        class CustomColors : ProfessionalColorTable
        {
            private Color _hoverColor = Color.FromArgb(150, 150, 150);

            public override Color MenuItemSelectedGradientBegin
            {
                get { return _hoverColor; }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return _hoverColor; }
            }

            public override Color MenuItemBorder
            {
                get { return _hoverColor; }
            }
        }
    }
}