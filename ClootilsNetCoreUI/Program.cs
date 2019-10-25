using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;

namespace ClootilsNetCoreUI
{
    public sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
#if DEBUG            
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
#else
                .UsePlatformDetect()
                .UseReactiveUI();
#endif
    }
}
