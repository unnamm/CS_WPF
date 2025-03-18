using Common.Attributes;
using Common.Config;
using Common.Message;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UI.Converts;
using UI.Model;

namespace UI.ViewModel
{
    public partial class SettingViewModel
    {
        public ObservableCollection<SettingTab> SettingTabs { get; } = [];

        private readonly List<YamlBase> _configList = [];

        public SettingViewModel()
        {
            AddSetting<SettingData>();
            AddSetting<SettingData2>();
        }

        [RelayCommand]
        public async Task Save()
        {
            List<Task> list = [];
            foreach (var config in _configList)
            {
                list.Add(config.SaveAsync());
            }
            await Task.WhenAll(list);
            WeakReferenceMessenger.Default.Send(new DialogMessage("Message", "all config save complete"));
        }

        private void AddSetting<T>() where T : YamlBase
        {
            var instance = Activator.CreateInstance<T>();
            _configList.Add(instance);

            try
            {
                instance.LoadAsync().Wait(); //auto load
            }
            catch { }

            var type = typeof(T);

            //set tab name
            SettingTab tab;
            var mapperAttr = type.GetCustomAttribute<SettingMapperAttribute>();
            if (mapperAttr == null)
            {
                tab = new SettingTab(type.Name);
            }
            else
            {
                tab = new SettingTab(mapperAttr.Name);
            }
            SettingTabs.Add(tab);

            foreach (var prop in type.GetProperties())
            {
                var memberAttr = prop.GetCustomAttribute<SettingMemberAttribute>();
                if (memberAttr == null)
                    continue;

                FrameworkElement? fe = memberAttr.Converter switch
                {
                    ConvertType.Text => CreateTextBox(prop, instance, memberAttr.Metadata),
                    ConvertType.Combo => CreateComboBox(prop, instance, memberAttr.Metadata),
                    ConvertType.Radio => CreateRadioButton(prop, instance, memberAttr.Metadata),
                    _ => null,
                };

                tab.Settings.Add(new SettingItem(memberAttr.Name, fe!));
            }

        }

        private static TextBox CreateTextBox(PropertyInfo propertyInfo, object instance, string[] metadata)
        {
            var textBox = new TextBox
            {
                Margin = new Thickness(5)
            };

            // 메타데이터에서 텍스트가 있다면 힌트 필드로 적용
            if (metadata.Length > 0)
            {
                textBox.SetValue(HintAssist.HintProperty, metadata[0]);
            }

            var binding = new Binding(propertyInfo.Name)
            {
                Source = instance,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            textBox.SetBinding(TextBox.TextProperty, binding);
            return textBox;
        }

        private static ComboBox CreateComboBox(PropertyInfo propertyInfo, object instance, string[] metadata)
        {
            var comboBox = new ComboBox
            {
                Margin = new Thickness(5),
                ItemsSource = metadata
            };

            var binding = new Binding(propertyInfo.Name)
            {
                Source = instance,
                Mode = BindingMode.TwoWay
            };

            comboBox.SetBinding(ComboBox.SelectedItemProperty, binding);
            return comboBox;
        }

        private static StackPanel CreateRadioButton(PropertyInfo propertyInfo, object instance, string[] metadata)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(5)
            };

            foreach (var option in metadata)
            {
                var radio = new RadioButton
                {
                    Content = option,
                    GroupName = propertyInfo.Name,
                    Margin = new Thickness(5, 0, 0, 0)
                };

                var binding = new Binding(propertyInfo.Name)
                {
                    Source = instance,
                    Mode = BindingMode.TwoWay
                };

                radio.SetBinding(RadioButton.IsCheckedProperty,
                    new Binding(propertyInfo.Name)
                    {
                        Source = instance,
                        Mode = BindingMode.TwoWay,
                        Converter = new RadioButtonConverter(option)
                    });

                stackPanel.Children.Add(radio);
            }

            return stackPanel;
        }

    }
}
