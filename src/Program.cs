using Microsoft.Win32;
using System.Windows.Forms;

namespace Nightlight
{
    class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegistryKey startupRegistry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            startupRegistry.SetValue("Nightlight", Application.ExecutablePath.ToString());
            Application.Run(new NightlightApp());
        }
    }
}
