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
            info.AppendLine( "Operating system:\t" + Environment.OSVersion );

            try
            {
                info.AppendLine();
                info.AppendLine( "OPENCL INFO" );

                foreach( ComputePlatform platform in ComputePlatform.Platforms )
                {
                    info.AppendLine( "Name:\t\t" + platform.Name );
                    info.AppendLine( "Vendor:\t\t" + platform.Vendor );
                    info.AppendLine( "Version:\t\t" + platform.Version );
                    info.AppendLine( "Profile:\t\t" + platform.Profile );
                    info.AppendLine( "Extensions:" );

                    foreach( string extension in platform.Extensions )
                        info.AppendLine( " + " + extension );

                    info.AppendLine();

                    info.AppendLine( "Devices:" );
                    foreach( ComputeDevice device in platform.Devices )
                    {
                        info.AppendLine( "\tName:\t" + device.Name );
                        info.AppendLine( "\tDriver:\t" + device.DriverVersion );                        
                        info.AppendLine( "\tExtensions:" );

                        foreach( string extension in device.Extensions )
                            info.AppendLine( "\t + " + extension );
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