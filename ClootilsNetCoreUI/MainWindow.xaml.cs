using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ClootilsNetCoreUI.VS2017
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
#if DEBUG            
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}