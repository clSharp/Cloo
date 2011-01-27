#region License

/*

Copyright (c) 2009 - 2011 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

using System;
using System.Windows.Forms;
using Cloo;

namespace Clootils
{
    public partial class ConfigForm : Form
    {
        public string Options { get; private set; }
        public ComputeDevice[] Devices { get; private set; }
        public ComputePlatform Platform { get; private set; }

        private bool[] devicesBackup = new bool[0];
        private int platformBackup = 0;
        private string optionsBackup = "";

        public ConfigForm()
        {
            InitializeComponent();

            deviceCheckList.CheckOnClick = true;
            deviceCheckList.SelectedIndexChanged += new EventHandler(deviceCheckList_SelectedIndexChanged);

            Platform = ComputePlatform.Platforms[0];
            platformComboBox.SelectedIndexChanged += new EventHandler(platformComboBox_SelectedIndexChanged);

            object[] availablePlatforms = new object[ComputePlatform.Platforms.Count];
            for (int i = 0; i < availablePlatforms.Length; i++) 
                availablePlatforms[i] = ComputePlatform.Platforms[i].Name;
            platformComboBox.Items.AddRange(availablePlatforms);
            platformComboBox.SelectedIndex = 0;

            StoreState();

            Shown += new EventHandler(SettingsForm_Shown);
        }

        void deviceCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceCheckList.CheckedItems.Count == 0)
            {
                MessageBox.Show("No device specified!\n\nSelect one or more devices from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                okButton.Enabled = false;
            }
            else
                okButton.Enabled = true;
        }

        void platformComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputePlatform platform = ComputePlatform.Platforms[platformComboBox.SelectedIndex];
            object[] availableDevices = new object[platform.Devices.Count];
            for (int i = 0; i < availableDevices.Length; i++) 
                availableDevices[i] = platform.Devices[i].Name;
            deviceCheckList.Items.Clear();
            deviceCheckList.Items.AddRange(availableDevices);
            deviceCheckList.SetItemChecked(0, true);
        }

        void SettingsForm_Shown(object sender, EventArgs e)
        {
            LoadState();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            StoreState();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            LoadState();
        }

        private void LoadState()
        {
            platformComboBox.SelectedIndex = platformBackup;

            for (int i = 0; i < devicesBackup.Length; i++)
                deviceCheckList.SetItemChecked(i, devicesBackup[i]);

            optionsTextBox.Text = optionsBackup;
            
            okButton.Enabled = true;
        }

        private void StoreState()
        {
            platformBackup = platformComboBox.SelectedIndex;

            devicesBackup = new bool[deviceCheckList.Items.Count];
            for (int i = 0; i < devicesBackup.Length; i++)
            {
                devicesBackup[i] = false;
                if (deviceCheckList.GetItemChecked(i))
                    devicesBackup[i] = true;
            }

            optionsBackup = optionsTextBox.Text;

            Platform = ComputePlatform.Platforms[platformComboBox.SelectedIndex];
            Devices = new ComputeDevice[deviceCheckList.CheckedItems.Count];
            int k = 0;
            for (int i = 0; k < Devices.Length && i < Platform.Devices.Count; i++)
                if (deviceCheckList.GetItemChecked(i))
                    Devices[k++] = Platform.Devices[i];

            Options = optionsTextBox.Text;
        }
    }
}