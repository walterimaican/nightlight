using System;
// using System.Collections.Generic;
// using System.Configuration;
// using System.Data;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace Nightlight
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NightlightApplication());
        }
    }

    class NightlightApplication : ApplicationContext
    {
        private NotifyIcon nightlight;

        public NightlightApplication()
        {
            nightlight = new NotifyIcon();
            nightlight.Icon = new Icon("assets/lightswitch.ico");
            nightlight.ContextMenuStrip = new ContextMenuStrip();
            nightlight.ContextMenuStrip.Items.Add("Exit", Image.FromFile("assets/lightswitch.ico"), myExit);
            nightlight.Visible = true;
        }

        void myExit(object sender, EventArgs e)
        {
            nightlight.Visible = false;
            Application.Exit();
        }
    }
}
