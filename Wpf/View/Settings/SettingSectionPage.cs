using Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace View.Settings
{
    public class SettingSectionPage<T> : Page where T : class, IConfigSection
    {
        public ObservableCollection<SettingFieldViewModel> Fields { get; } = [];

        public SettingSectionPage(IOptionsMonitor<T> monitor)
        {
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

            var saveButton = new Wpf.Ui.Controls.Button
            {
                Content = "저장",
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            saveButton.Click += (_, _) => monitor.Save();

            var stack = new StackPanel { Margin = new Thickness(20) };
            stack.Children.Add(itemsControl);
            stack.Children.Add(saveButton);

            Content = stack;
        }
    }
}
