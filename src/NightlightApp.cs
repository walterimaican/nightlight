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
        private ToolStripMenuItem _settingsMenu;
        private CustomToolStripSeparator[] _separators;
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

            _settingsMenu = new ToolStripMenuItem();
            _settingsMenu.Image = Image.FromFile(SETTINGS_ICON_PATH);
            _settingsMenu.Text = "Settings";
            _settingsMenu.DropDownItems.Add(appsThemeButton);
            _settingsMenu.DropDownItems.Add(systemThemeButton);

            ToolStripMenuItem aboutButton = new ToolStripMenuItem();
            aboutButton.Image = Image.FromFile(HELP_ICON_PATH);
            aboutButton.Text = "Help && About";
            aboutButton.Click += OnHelpAndAbout;

            ToolStripMenuItem exitButton = new ToolStripMenuItem();
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;

            _separators = new CustomToolStripSeparator[]
            {
                new CustomToolStripSeparator(),
                new CustomToolStripSeparator()
            };

            ToolStripItem[] toolStripItems = new ToolStripItem[] {
                lightModeButton,
                darkModeButton,
                _separators[0],
                _settingsMenu,
                aboutButton,
                _separators[1],
                exitButton
            };

            // Context Menu
            _contextMenuStrip = new ContextMenuStrip();
            _contextMenuStrip.ShowItemToolTips = false;
            _contextMenuStrip.Items.AddRange(toolStripItems);
            _contextMenuStrip.Renderer = new CustomRenderer();

            // Notify Icon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = _contextMenuStrip;
            _notifyIcon.Click += OnIconClick;

            // Initialize UI
            if (_themeSwitcher.GetIsLight())
            {
                ActivateLightMode();
            }
            else
            {
                ActivateDarkMode();
            }
        }

        private void ActivateLightMode()
        {
            foreach (ToolStripItem item in _contextMenuStrip.Items)
            {
                item.BackColor = LIGHT_MODE_BACKGROUND;
                item.ForeColor = LIGHT_MODE_TEXT;
            }
            foreach (ToolStripMenuItem item in _settingsMenu.DropDownItems)
            {
                item.BackColor = LIGHT_MODE_BACKGROUND;
                item.ForeColor = LIGHT_MODE_TEXT;
            }
            foreach (CustomToolStripSeparator item in _separators)
            {
                item.BackColor = LIGHT_MODE_BACKGROUND;
                item.ForeColor = LIGHT_MODE_TEXT;
            }
            _notifyIcon.Icon = _lightIcon;
            _notifyIcon.Text = ICON_LIGHT_TEXT;

            _themeSwitcher.SetThemeToLight();
        }

        private void ActivateDarkMode()
        {
            foreach (ToolStripItem item in _contextMenuStrip.Items)
            {
                item.BackColor = DARK_MODE_BACKGROUND;
                item.ForeColor = DARK_MODE_TEXT;
            }
            foreach (ToolStripMenuItem item in _settingsMenu.DropDownItems)
            {
                item.BackColor = DARK_MODE_BACKGROUND;
                item.ForeColor = DARK_MODE_TEXT;
            }
            foreach (CustomToolStripSeparator item in _separators)
            {
                item.BackColor = DARK_MODE_BACKGROUND;
                item.ForeColor = DARK_MODE_TEXT;
            }
            _notifyIcon.Icon = _darkIcon;
            _notifyIcon.Text = ICON_DARK_TEXT;

            _themeSwitcher.SetThemeToDark();
        }

        void OnIconClick(object sender, EventArgs e)
        {
            // If icon is right-clicked, user is opening context menu, do not toggle Nightlight
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                return;
            }

            // Toggle Nightlight
            if (_themeSwitcher.GetIsLight())
            {
                ActivateDarkMode();
            }
            else
            {
                ActivateLightMode();
            }
        }

        void OnLightMode(object sender, EventArgs e)
        {
            ActivateLightMode();
        }

        void OnDarkMode(object sender, EventArgs e)
        {
            ActivateDarkMode();
        }

        void OnAppsToggle(object sender, EventArgs e)
        {
            ToolStripMenuItem appsButton = sender as ToolStripMenuItem;
            _themeSwitcher.SetShouldToggleApps(appsButton.Checked);
        }

        void OnSystemToggle(object sender, EventArgs e)
        {
            ToolStripMenuItem systemButton = sender as ToolStripMenuItem;
            _themeSwitcher.SetShouldToggleSystem(systemButton.Checked);

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
    }

    class CustomRenderer : ToolStripProfessionalRenderer
    {
        public CustomRenderer() : base(new CustomColors()) { }
    }

    class CustomColors : ProfessionalColorTable
    {
        private Color _hoverColor = Color.FromArgb(150, 150, 150);

        // Gradient used here - MenuItemSelected does not work
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

    class CustomToolStripSeparator : ToolStripSeparator
    {
        // Override the base class values
        new public Color BackColor;
        new public Color ForeColor;

        public CustomToolStripSeparator()
        {
            this.Paint += CustomPaintHandler;
        }

        private void CustomPaintHandler(object sender, PaintEventArgs e)
        {
            // Get dimensions of old separator
            ToolStripSeparator oldSeparator = sender as ToolStripSeparator;
            int width = oldSeparator.Width;
            int height = oldSeparator.Height;

            // Paint new separator
            e.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);
            e.Graphics.DrawLine(new Pen(ForeColor), 4, height / 2, width - 4, height / 2);
        }
    }
}