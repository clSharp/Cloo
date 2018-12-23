using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;

using ReactiveUI;
using ReactiveUI.Legacy;
using Cloo.Extensions;
using ClootilsNetCoreUI.VS2017.Properties;


// NOTE: https://github.com/AvaloniaUI/Avalonia/blob/master/samples/BindingDemo/ViewModels/MainWindowViewModel.cs
namespace ClootilsNetCoreUI.VS2017
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Dictionary<string, Func<int, string>> tests = new  Dictionary<string, Func<int, string>>()
        {
            {
                "Calculate tons of primes - up to 10000000", 
                new Func<int, string>((deviceIndex)=>
                {
                    var primes = Enumerable.Range(2, 10000000).ToArray();
                    primes.ClooForEach(Resources.IsPrime, null, (i, d, v) => i == deviceIndex);

                    return string.Join(", ", primes.Take(30).Where(n => n != 0).Where(n => n != 0))
                     + ", ... ," + string.Join(", ", primes.Skip(primes.Length-30).Where(n => n != 0));
                })
            }
        }; 

        public MainWindowViewModel()
        {
            this.Items = new ReactiveList<TestItem>(
                this.tests.Select(t => new TestItem
                {
                    StringValue = t.Key
                }));

            this.Platforms = new ReactiveList<TestItem>(
                ClooExtensions.GetDeviceNames().Select((d, i) => new TestItem
                {
                    StringValue = d
                }));

            this.SelectedItems = new ReactiveList<TestItem>();

            this.SelectedPlatformIndex=0;

            this.RunItems = ReactiveCommand.CreateFromTask(async () =>
            {                
                var selectedTestNames = this.SelectedItems.Select(si => si.StringValue).ToList();
                this.ResultText = $"Started {selectedTestNames.Count} test(s)... {Environment.NewLine}";

                var sw = new Stopwatch();
                foreach(var testName in selectedTestNames)
                {
                    sw.Restart();
                    try
                    {
                        this.ResultText += await Task.Run(() => this.tests[testName](this.selectedPlatformIndex));
                    }
                    catch (Exception ex)
                    {
                        this.ResultText += ex.ToString();
                    }
                    finally
                    {
                        sw.Stop();
                    }                    
                    this.ResultText += $"{Environment.NewLine}Time({testName}): {sw.Elapsed}";
                }

                this.ResultText += $"{Environment.NewLine} All {selectedTestNames.Count} test(s) done!";
            }, this.SelectedItems.CountChanged.Select(count => count != 0));
        }

        private int selectedPlatformIndex;
        public int SelectedPlatformIndex
        {
            get { return selectedPlatformIndex; }
            set { this.RaiseAndSetIfChanged(ref selectedPlatformIndex, value); }
        }

        private string resultText;
        public string ResultText
        {
            get { return resultText; }
            set { this.RaiseAndSetIfChanged(ref resultText, value); }
        }

        public ReactiveList<TestItem> Items { get; }
        public ReactiveList<TestItem> Platforms { get; }
        public ReactiveList<TestItem> SelectedItems { get; }
        public ICommand RunItems { get; }

        public class TestItem : ReactiveObject
        {
            private string stringValue;

            public string StringValue
            {
                get { return stringValue; }
                set { this.RaiseAndSetIfChanged(ref this.stringValue, value); }
            }

            public override string ToString()
            {
                return stringValue;
            }
        }
    }    
}