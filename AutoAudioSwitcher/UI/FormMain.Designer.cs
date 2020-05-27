namespace AutoAudioSwitcher.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.devicesType = new System.Windows.Forms.ComboBox();
            this.createShortcutButton = new System.Windows.Forms.Button();
            // 
            // devicesType
            // 
            this.devicesType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicesType.FormattingEnabled = true;
            this.devicesType.Items.AddRange(new object[] {
            "Output",
            "Input"});
            this.devicesType.Location = new System.Drawing.Point(6, 6);
            this.devicesType.Name = "devicesType";
            this.devicesType.Size = new System.Drawing.Size(300, 28);
            this.devicesType.TabIndex = 1;
            this.devicesType.SelectedIndexChanged += new System.EventHandler(this.DevicesTypeChanged);
            // 
            // createShortcutButton
            // 
            this.createShortcutButton.Enabled = false;
            this.createShortcutButton.Location = new System.Drawing.Point(6, 248);
            this.createShortcutButton.Name = "createShortcutButton";
            this.createShortcutButton.Size = new System.Drawing.Size(300, 29);
            this.createShortcutButton.TabIndex = 2;
            this.createShortcutButton.Text = "Create enabling shortcut";
            this.createShortcutButton.UseVisualStyleBackColor = true;
            this.createShortcutButton.Click += new System.EventHandler(this.CreateShortcutButton_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 283);
            this.Controls.Add(this.createShortcutButton);
            this.Controls.Add(this.devicesType);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoAudioSwitcher";

        }

        #endregion

        private void InitializeComponentEnd()
        {
            audioDevices.ItemCheck += DeviceChecked;
            audioDevices.MouseDown += AudioDevicesMouseClicked;
            audioDevices.ContextMenuStrip.Items[0].Click += EnableDeviceClicked;
            Controls.Add(audioDevices);
            devicesType.SelectedIndex = 0;
            DoubleBuffered = true;
        }

        private System.Windows.Forms.ComboBox devicesType;
        private Controls.MMDeviceBox audioDevices = new UI.Controls.MMDeviceBox
        {
            FormattingEnabled = true,
            Location = new System.Drawing.Point(6, 40),
            Name = "audioDevices",
            DisplayMember = nameof(CoreAudio.MMDevice.FriendlyName),
            ScrollAlwaysVisible = true,
            Size = new System.Drawing.Size(300, 202),
            TabIndex = 2,
            SelectedDeviceBackColor = System.Drawing.Color.LightGreen,
            ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip
            { 
                Items = { "Enable" } 
            }
        };
        private System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = ".lnk",
            Filter = "*.lnk|Shortcuts",
            ValidateNames = true,
            Title = "Choose where to save the shortcut"
        };
        private System.Windows.Forms.Button createShortcutButton;
    }
}