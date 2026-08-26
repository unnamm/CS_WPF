using Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace View.Settings
{
    public class SettingSectionPage<T> : Page, INavigationAware where T : class, IConfigSection
    {
        readonly IOptionsMonitor<T> _monitor;

        public ObservableCollection<SettingFieldViewModel> Fields { get; } = [];

        public SettingSectionPage(IOptionsMonitor<T> monitor)
        {
            _monitor = monitor;
            Title = typeof(T).Name;

            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/View;component/Settings/SettingFieldTemplates.xaml", UriKind.Relative)
            });

            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanRead || !property.CanWrite)
                    continue;

                Fields.Add(new SettingFieldViewModel(monitor.CurrentValue, property));
            }

            var itemsControl = new ItemsControl
            {
                ItemsSource = Fields,
                ItemTemplateSelector = new SettingFieldTemplateSelector()
            };

            var stack = new StackPanel { Margin = new Thickness(20) };
            stack.Children.Add(itemsControl);

            Content = stack;
        }

        public Task OnNavigatedToAsync() => Task.CompletedTask;

        public Task OnNavigatedFromAsync()
        {
            _monitor.Save();
            return Task.CompletedTask;
        }
    }
}
