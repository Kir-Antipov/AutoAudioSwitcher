using CoreAudio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutoAudioSwitcher
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if ((args?.Length ?? 0) == 0)
                RunForm();
            else
                RunProgram(args!);
        }

        private static void RunForm()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.FormMain());
        }

        private static void RunProgram(string[] args)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice[] devices = enumerator.EnumerateAudioEndPoints(EDataFlow.eAll, DEVICE_STATE.DEVICE_STATE_ACTIVE).ToArray();
            devices = Array.ConvertAll(args, id => devices.FirstOrDefault(device => string.Equals(device.ID, id, StringComparison.InvariantCultureIgnoreCase)));

            int selectedIndex = Array.FindIndex(devices, x => x?.Selected is true);
            if (selectedIndex == -1)
                selectedIndex = -1;

            devices[(selectedIndex + 1) % devices.Length].Selected = true;
        }
    }
}
