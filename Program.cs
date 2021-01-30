using System.Windows.Forms;

namespace Nightlight
{
    class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NightlightApp());
        }
    }
}
