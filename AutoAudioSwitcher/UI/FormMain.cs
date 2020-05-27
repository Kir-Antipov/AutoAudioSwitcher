using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreAudio;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace AutoAudioSwitcher.UI
{
    public sealed partial class FormMain : Form
    {
        #region Var
        private static readonly EDataFlow[] DeviceTypes = { EDataFlow.eRender, EDataFlow.eRender };
        private EDataFlow ActiveDeviceType => DeviceTypes[devicesType.SelectedIndex];
        private IEnumerable<MMDevice> ActiveDevices => DeviceEnumerator.EnumerateAudioEndPoints(ActiveDeviceType, DEVICE_STATE.DEVICE_STATE_ACTIVE);

        private readonly MMDeviceEnumerator DeviceEnumerator;
        #endregion

        #region Init
        public FormMain()
        {
            InitializeComponent();

            DeviceEnumerator = new MMDeviceEnumerator();

            InitializeComponentEnd();
        }
        #endregion

        #region Functions
        private async void UpdateButtonState()
        {
            await Task.Delay(1);
            createShortcutButton.Enabled = audioDevices.CheckedIndices.Count > 0;
        }

        private async void DevicesTypeChanged(object sender, EventArgs e)
        {
            audioDevices.Clear();
            UpdateButtonState();
            foreach (MMDevice device in ActiveDevices)
                audioDevices.Items.Add(device);

            await Task.Delay(1);
            audioDevices.Focus();
        }

        private void EnableDeviceClicked(object sender, EventArgs e)
        {
            if (audioDevices.SelectedIndex != -1)
            {
                DeviceEnumerator.SetDefaultAudioEndpoint(audioDevices[audioDevices.SelectedIndex]);
                DevicesTypeChanged(sender, e);
            }
        }

        private void DeviceChecked(object sender, ItemCheckEventArgs e) => UpdateButtonState();

        private void AudioDevicesMouseClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = audioDevices.IndexFromPoint(e.X, e.Y);
                if (index != ListBox.NoMatches)
                    audioDevices.SelectedIndex = index;
            }
        }

        private void CreateShortcutButton_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            if (string.IsNullOrEmpty(fileDialog.FileName))
                return;

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(fileDialog.FileName);
            shortcut.TargetPath = Path.ChangeExtension(new Uri(typeof(Program).Assembly.CodeBase!, UriKind.Absolute).LocalPath, ".exe");
            shortcut.WorkingDirectory = Path.GetDirectoryName(shortcut.TargetPath);
            shortcut.Arguments = string.Join(' ', audioDevices.CheckedItems.OfType<MMDevice>().Select(x => x.ID));
            string defaultIcons = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "SHELL32.dll");
            if (File.Exists(defaultIcons))
            {
                shortcut.IconLocation = $"{defaultIcons},168";
            }

            shortcut.Save();
        }
        #endregion
    }
}
