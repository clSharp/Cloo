using System;
using System.Windows.Forms;
using Cloo;
using System.Text;

namespace ClooInfo
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void copyButton_Click( object sender, EventArgs e )
        {
            Clipboard.Clear();
            Clipboard.SetText( textBox.Text );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine( "HOST INFO" );
            info.AppendLine( "Operating system: " + Environment.OSVersion );

            try
            {
                info.AppendLine();
                info.AppendLine( "OPENCL PLATFORMS" );

                foreach( ComputePlatform platform in ComputePlatform.Platforms )
                {
                    info.AppendLine( "Name: " + platform.Name );
                    info.AppendLine( "Vendor: " + platform.Vendor );
                    info.AppendLine( "Version: " + platform.Version );
                    info.AppendLine( "Profile: " + platform.Profile );
                    info.AppendLine( "Extensions:" );

                    foreach( string extension in platform.Extensions )
                        info.AppendLine( " + " + extension );

                    info.AppendLine();

                    info.AppendLine( "Devices:" );
                    foreach( ComputeDevice device in platform.Devices )
                    {
                        info.AppendLine( "\tName: " + device.Name );
                        info.AppendLine( "\tVendor: " + device.Vendor );
                        info.AppendLine( "\tDriver version: " + device.DriverVersion );
                        info.AppendLine( "\tGlobal memory: " + device.GlobalMemorySize );
                        info.AppendLine( "\tLocal memory: " + device.LocalMemorySize );
                        info.AppendLine( "\tImage support: " + device.ImageSupport );
                        info.AppendLine( "\tCompute units: " + device.MaxComputeUnits );
                        info.AppendLine( "\tExtensions:" );

                        foreach( string extension in device.Extensions )
                            info.AppendLine( "\t + " + extension );

                        info.AppendLine();
                    }
                }
            }

            catch( ComputeException exception )
            {
                info.Append( exception.StackTrace );
            }

            textBox.Text = info.ToString();
        }
    }
}