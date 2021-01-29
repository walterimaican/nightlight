using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nightlight
{
    class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NightlightApplication());
        }
    }

    class NightlightApplication : ApplicationContext
    {
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private Icon _lightIcon;
        private Icon _darkIcon;
        private Boolean _isLight;
        private ToolStripLabel _status;

        private const String LIGHT_STATUS = "Current Mode: Light";
        private const String DARK_STATUS = "Current Mode: Dark";
        private const String ICON_LIGHT_TEXT = "Nightlight: Light";
        private const String ICON_DARK_TEXT = "Nightlight: Dark";
        private Color LIGHT_MODE_BACKGROUND = Color.White;
        private Color DARK_MODE_BACKGROUND = Color.Black;
        private Color LIGHT_MODE_TEXT = Color.Black;
        private Color DARK_MODE_TEXT = Color.White;

        public NightlightApplication()
        {
            /* todo logic here */
            _isLight = true;

            // Icons
            String lightIconPath = "assets/light.ico";
            String darkIconPath = "assets/dark.ico";
            _lightIcon = new Icon(lightIconPath);
            _darkIcon = new Icon(darkIconPath);

            // Context Menu Items
            _status = new ToolStripLabel();
            _status.Text = _isLight ? LIGHT_STATUS : DARK_STATUS;

            ToolStripMenuItem lightModeButton = new ToolStripMenuItem();
            lightModeButton.Image = Image.FromFile(lightIconPath);
            lightModeButton.Text = " Activate Light Mode";
            lightModeButton.Click += OnLightMode;

            ToolStripMenuItem darkModeButton = new ToolStripMenuItem();
            darkModeButton.Image = Image.FromFile(darkIconPath);
            darkModeButton.Text = " Activate Dark Mode";
            darkModeButton.Click += OnDarkMode;

            ToolStripMenuItem aboutButton = new ToolStripMenuItem();
            aboutButton.Text = "About";
            aboutButton.Click += OnAbout;

            ToolStripMenuItem exitButton = new ToolStripMenuItem();
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;

            ToolStripItem[] toolStripItems = new ToolStripItem[] {
                _status,
                new ToolStripSeparator(),
                lightModeButton,
                darkModeButton,
                new ToolStripSeparator(),
                aboutButton,
                exitButton
            };

            // Context Menu
            _contextMenuStrip = new ContextMenuStrip();
            _contextMenuStrip.ShowCheckMargin = false;
            _contextMenuStrip.ShowItemToolTips = false;
            _contextMenuStrip.BackColor = _isLight ? LIGHT_MODE_BACKGROUND : DARK_MODE_BACKGROUND;
            _contextMenuStrip.ForeColor = _isLight ? LIGHT_MODE_TEXT : DARK_MODE_TEXT;
            _contextMenuStrip.Items.AddRange(toolStripItems);

            // Notify Icon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = _isLight ? ICON_LIGHT_TEXT : ICON_DARK_TEXT;
            _notifyIcon.Icon = _isLight ? _lightIcon : _darkIcon;
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenuStrip = _contextMenuStrip;
            _notifyIcon.Click += OnIconClick;
        }

        private void activateLightMode()
        {
            _contextMenuStrip.BackColor = LIGHT_MODE_BACKGROUND;
            _contextMenuStrip.ForeColor = LIGHT_MODE_TEXT;
            _notifyIcon.Icon = _lightIcon;
            _status.Text = LIGHT_STATUS;
            _notifyIcon.Text = ICON_LIGHT_TEXT;
            _isLight = true;
        }

        private void activateDarkMode()
        {
            _contextMenuStrip.BackColor = DARK_MODE_BACKGROUND;
            _contextMenuStrip.ForeColor = DARK_MODE_TEXT;
            _notifyIcon.Icon = _darkIcon;
            _status.Text = DARK_STATUS;
            _notifyIcon.Text = ICON_DARK_TEXT;
            _isLight = false;
        }

        void OnIconClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                return;
            }

            if (_isLight)
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

        void OnAbout(object sender, EventArgs e)
        {
            // TODO
        }

        void OnExit(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            Application.Exit();
        }
    }
}
