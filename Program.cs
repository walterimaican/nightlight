﻿using System;
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
        private static bool _isLight;
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private Icon _lightIcon;
        private Icon _darkIcon;

        /* Constants */
        private const String lightIconPath = "assets/light.ico";
        private const String darkIconPath = "assets/dark.ico";
        private const String ICON_LIGHT_TEXT = "Nightlight: Light";
        private const String ICON_DARK_TEXT = "Nightlight: Dark";
        private Color LIGHT_MODE_BACKGROUND = Color.WhiteSmoke;
        private Color DARK_MODE_BACKGROUND = Color.FromArgb(50, 50, 50);
        private Color LIGHT_MODE_TEXT = Color.Black;
        private Color DARK_MODE_TEXT = Color.WhiteSmoke;

        public NightlightApplication()
        {
            _isLight = isCurrentlyLight();

            // Icons
            _lightIcon = new Icon(lightIconPath);
            _darkIcon = new Icon(darkIconPath);

            // Context Menu Items
            ToolStripMenuItem lightModeButton = new ToolStripMenuItem();
            lightModeButton.Image = Image.FromFile(lightIconPath);
            lightModeButton.Text = "Activate Light Mode";
            lightModeButton.Click += OnLightMode;

            ToolStripMenuItem darkModeButton = new ToolStripMenuItem();
            darkModeButton.Image = Image.FromFile(darkIconPath);
            darkModeButton.Text = "Activate Dark Mode";
            darkModeButton.Click += OnDarkMode;

            ToolStripMenuItem aboutButton = new ToolStripMenuItem();
            aboutButton.Text = "About";
            aboutButton.Click += OnAbout;

            ToolStripMenuItem exitButton = new ToolStripMenuItem();
            exitButton.Text = "Exit";
            exitButton.Click += OnExit;

            ToolStripItem[] toolStripItems = new ToolStripItem[] {
                lightModeButton,
                darkModeButton,
                aboutButton,
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

            // Initialize
            if (_isLight)
            {
                activateLightMode();
            }
            else
            {
                activateDarkMode();
            }
        }

        private bool isCurrentlyLight()
        {
            // TODO
            return true;
        }

        private void activateLightMode()
        {
            foreach (ToolStripItem item in _contextMenuStrip.Items)
            {
                item.BackColor = LIGHT_MODE_BACKGROUND;
                item.ForeColor = LIGHT_MODE_TEXT;
            }
            _notifyIcon.Icon = _lightIcon;
            _notifyIcon.Text = ICON_LIGHT_TEXT;
            _isLight = true;
        }

        private void activateDarkMode()
        {
            foreach (ToolStripItem item in _contextMenuStrip.Items)
            {
                item.BackColor = DARK_MODE_BACKGROUND;
                item.ForeColor = DARK_MODE_TEXT;
            }
            _notifyIcon.Icon = _darkIcon;
            _notifyIcon.Text = ICON_DARK_TEXT;
            _isLight = false;
        }

        void OnIconClick(object sender, EventArgs e)
        {
            // If icon is right-clicked, user is opening context menu, do not toggle Nightlight
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                return;
            }

            // Toggle Nightlight
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
        class CustomRenderer : ToolStripProfessionalRenderer
        {
            public CustomRenderer() : base(new CustomColors()) { }
        }

        class CustomColors : ProfessionalColorTable
        {
            private Color _hoverColor = Color.FromArgb(175, 175, 175);

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
