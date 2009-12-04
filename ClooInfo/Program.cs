using System;
namespace ClooInfo
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            System.Windows.Forms.Application.Run( new MainForm() );
        }
    }
}