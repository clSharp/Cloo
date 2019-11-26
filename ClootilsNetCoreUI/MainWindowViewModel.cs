using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cloo.Extensions;
using ClootilsNetCoreUI.Properties;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace ClootilsNetCoreUI
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Dictionary<string, Func<int, string>> tests = new  Dictionary<string, Func<int, string>>()
        {
            {
                "Calculate some primes", 
                new Func<int, string>((deviceIndex)=>
                {
                    var primes = Enumerable.Range(2, 1000).ToArray();
                    primes.ClooForEach(Resources.IsPrime, null, (i, d, v) => i == deviceIndex);

                    return string.Join(", ", primes.Take(30).Where(n => n != 0).Where(n => n != 0))
                     + ", ... ," + string.Join(", ", primes.Skip(primes.Length-30).Where(n => n != 0));
                })
            },
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
            var items = new SourceList<TestItem>();
            items.AddRange(this.tests.Select(t => new TestItem
                {
                    StringValue = t.Key
                }));
            items.Connect().Bind(this.Items).Subscribe();

            var platforms = new SourceList<TestItem>();
            platforms.AddRange(ClooExtensions.GetDeviceNames().Select((d, i) => new TestItem
                {
                    StringValue = d.Trim()
                }));
            platforms.Connect().Bind(this.Platforms).Subscribe();

            var selectedItems = new SourceList<TestItem>();
            selectedItems.AddRange(new List<TestItem>());
            selectedItems.Connect().Bind(this.SelectedItems).Subscribe();

            this.SelectedPlatformIndex=0;

            this.RunItems = ReactiveCommand.CreateFromTask(async () =>
            {
                var selectedTestNames = this.SelectedItems.Select(si => si.StringValue).ToList();
                this.ResultText = $"Started {selectedTestNames.Count()} test(s)...";

                var sw = new Stopwatch();
                foreach(var testName in selectedTestNames)
                {
                    this.ResultText += $"{Environment.NewLine}";
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

                this.ResultText += $"{Environment.NewLine} All {selectedTestNames.Count()} test(s) done!";
            }, this.SelectedItems.ObserveCollectionChanges().Select(x => this.SelectedItems.Any()));
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

        public IObservableCollection<TestItem> Items { get; } = new ObservableCollectionExtended<TestItem>();
        public IObservableCollection<TestItem> Platforms { get; } = new ObservableCollectionExtended<TestItem>();
        public IObservableCollection<TestItem> SelectedItems { get; } = new ObservableCollectionExtended<TestItem>();
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