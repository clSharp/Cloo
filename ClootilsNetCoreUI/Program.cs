using Avalonia;
using Avalonia.Logging.Serilog;

namespace ClootilsNetCoreUI
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>();
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
