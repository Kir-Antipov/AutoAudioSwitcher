using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using CoreAudio;

namespace AutoAudioSwitcher.UI.Controls
{
    public partial class MMDeviceBox : CheckedListBox
    {
        public Color SelectedDeviceForeColor
        {
            get => _selectedDeviceForeColor;
            set
            {
                _selectedDeviceForeColor = value;
                Invalidate();
            }
        }
        private Color _selectedDeviceForeColor = DefaultForeColor;

        public Color SelectedDeviceBackColor
        {
            get => _selectedDeviceBackColor;
            set
            {
                _selectedDeviceBackColor = value;
                Invalidate();
            }
        }
        private Color _selectedDeviceBackColor = SystemColors.ControlLightLight;

        public MMDevice this[int i] => Items.Count > i ? Items[i] is MMDevice device ? device : throw new InvalidCastException() : throw new IndexOutOfRangeException(); 

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                if (Items[e.Index] is MMDevice { Selected: true } audioDevice)
                {
                    OnDrawItemBox(new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State, SelectedDeviceForeColor, SelectedDeviceBackColor));
                }
                else
                    OnDrawItemBox(e);
            }
        }

        public virtual void Clear()
        {
            StateCache.Clear();
            Items.Clear();
        }

        private readonly Dictionary<int, (bool Checked, bool Selected)> StateCache = new Dictionary<int, (bool Checked, bool Selected)>();

        protected virtual void OnDrawItemBox(DrawItemEventArgs e)
        {
            bool isChecked = GetItemChecked(e.Index);
            bool isSelected = e.State.HasFlag(DrawItemState.Selected);

            if (!StateCache.TryGetValue(e.Index, out var state))
                state = (!isChecked, !isSelected);

            if (state.Selected != isSelected || state.Checked != isChecked)
            {
                if (state.Selected != isSelected)
                {
                    e.DrawBackground();
                    using StringFormat format = new StringFormat { LineAlignment = StringAlignment.Center };
                    using Brush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString((Items[e.Index] as MMDevice)?.FriendlyName, Font, brush, new Rectangle(e.Bounds.Height, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height), format);
                }

                Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, CheckBoxState.MixedNormal);
                int dx = (e.Bounds.Height - checkSize.Width) / 2;
                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(dx, e.Bounds.Top + dx), isChecked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
                StateCache[e.Index] = (isChecked, isSelected);
            }
        }
    }
}
