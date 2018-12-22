using Avalonia;
using Avalonia.Markup.Xaml;
using ReactiveUI;

namespace ClootilsNetCoreUI.VS2017
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
   }
}