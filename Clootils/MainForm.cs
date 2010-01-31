#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Cloo;
using Clootils.Properties;

namespace Clootils
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitializeSettings();

            editorTextBox.AcceptsReturn = true;
            editorTextBox.AcceptsTab = true;
            editorTextBox.Font = Settings.Default.EditorFont;
                        
            fontDialog.FontMustExist = true;

            logTextBox.Font = Settings.Default.LogFont;

            openFileDialog.Multiselect = false;

            saveFileDialog.OverwritePrompt = true;
        }

        private void InitializeSettings()
        {
            if( Settings.Default.EditorFont == null )
                Settings.Default.EditorFont = new Font( FontFamily.GenericMonospace, 10 );

            if( Settings.Default.LogFont == null )
                Settings.Default.LogFont = new Font( FontFamily.GenericMonospace, 10 );

            Settings.Default.Save();
        }

        private void openFileMenuItem_Click( object sender, EventArgs e )
        {
            if( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                FileStream file = new FileStream( openFileDialog.FileName, FileMode.Open, FileAccess.Read );
                StreamReader reader = new StreamReader( file );
                editorTextBox.Lines = ParseLines( reader.ReadToEnd() );
                reader.Close();
                file.Close();
            }
        }

        private void saveFileMenuItem_Click( object sender, EventArgs e )
        {
            if( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                FileStream file = new FileStream( saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None );
                StreamWriter writer = new StreamWriter( file );

                foreach( string line in editorTextBox.Lines )
                    writer.WriteLine( line );

                writer.Close();
                file.Close();
            }
        }

        private void buildProgramMenuItem_Click( object sender, EventArgs e )
        {
            if( editorTextBox.Text.Length < 1 ) return;

            string[] logContent;

            ComputeContextPropertyList properties = new ComputeContextPropertyList( ComputePlatform.Platforms[ 0 ] );
            ComputeContext context = new ComputeContext( ComputeDeviceTypes.All, properties, null, IntPtr.Zero );
            ComputeProgram program = new ComputeProgram( context, editorTextBox.Lines );
            try
            {
                program.Build( null, null, null, IntPtr.Zero );
                logContent = new string[] { "build succeeded" };
            }
            catch( ComputeException exception )
            {
                List<string> lineList = new List<string>();
                foreach( ComputeDevice device in context.Devices )
                {
                    char[] delimiter = new char[ device.Name.Length ];
                    for( int i = 0; i < delimiter.Length; i++ ) delimiter[ i ] = '-';

                    lineList.Add( new string( delimiter ) );
                    lineList.Add( device.Name );
                    lineList.Add( new string( delimiter ) );

                    StringReader reader = new StringReader( program.GetBuildLog( device ) );                    
                    string line = reader.ReadLine();
                    while( line != null )
                    {
                        lineList.Add( line );
                        line = reader.ReadLine();
                    }
                }
                logContent = lineList.ToArray();
            }

            logTextBox.Lines = logContent;
        }

        private void editorFontMenuItem_Click( object sender, EventArgs e )
        {
            fontDialog.Font = editorTextBox.Font;
            if( fontDialog.ShowDialog() == DialogResult.OK )
            {
                editorTextBox.Font = fontDialog.Font;
                Settings.Default.EditorFont = fontDialog.Font;
                Settings.Default.Save();
            }
        }

        private void logFontMenuItem_Click( object sender, EventArgs e )
        {
            fontDialog.Font = logTextBox.Font;
            if( fontDialog.ShowDialog() == DialogResult.OK )
            {
                logTextBox.Font = fontDialog.Font;
                Settings.Default.LogFont = fontDialog.Font;
                Settings.Default.Save();
            }
        }

        private void infoButton_Click( object sender, EventArgs e )
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine( "[HOST INFO]" );
            info.AppendLine( "Operating system: " + Environment.OSVersion );

            info.AppendLine();
            info.AppendLine( "[OPENCL PLATFORMS]" );

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
                info.AppendLine();
            }

            infoTextBox.Lines = ParseLines( info.ToString() );
        }

        private void testButton_Click( object sender, EventArgs e )
        {
            StringBuilder output = new StringBuilder();
            StringWriter writer = new StringWriter( output );

            Console.SetOut( writer );

            new DummyTest().Run();
            new MappingTest().Run();
            new ProgramTest().Run();
            new KernelsTest().Run();
            new VectorAddTest().Run();

            Console.SetOut( Console.Out );
            writer.Close();
            
            infoTextBox.Lines = ParseLines( output.ToString() );
        }

        private string[] ParseLines( string text )
        {
            List<string> lineList = new List<string>();
            StringReader reader = new StringReader( text );
            string line = reader.ReadLine();
            while( line != null )
            {
                lineList.Add( line );
                line = reader.ReadLine();
            }
            return lineList.ToArray();
        }

        private void copyButton_Click( object sender, EventArgs e )
        {
            Clipboard.Clear();
            Clipboard.SetText( infoTextBox.Text );
        }
    }
}