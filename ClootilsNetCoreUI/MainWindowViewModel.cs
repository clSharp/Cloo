using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

// NOTE: https://github.com/AvaloniaUI/Avalonia/blob/master/samples/BindingDemo/ViewModels/MainWindowViewModel.cs
namespace ClootilsNetCoreUI.VS2017
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<TestItem>(
                Enumerable.Range(0, 20).Select(x => new TestItem
                {
                    StringValue = "Item " + x,
                    Detail = "Item " + x + " details",
                }));

            SelectedItems = new ObservableCollection<TestItem>();
        }

        public ObservableCollection<TestItem> Items { get; }
        public ObservableCollection<TestItem> SelectedItems { get; }

        public class TestItem : ReactiveObject
        {
            private string _stringValue = "String Value";
            private string _detail;

            public string StringValue
            {
                get { return _stringValue; }
                set { this.RaiseAndSetIfChanged(ref this._stringValue, value); }
            }

            public string Detail
            {
                get { return _detail; }
                set { this.RaiseAndSetIfChanged(ref this._detail, value); }
            }
        }
    }    
}