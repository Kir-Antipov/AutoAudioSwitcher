using System;
using System.Windows.Forms;

namespace AutoAudioSwitcher
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.FormMain());
        }
    }
}
