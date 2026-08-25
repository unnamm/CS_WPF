using System;
using System.Windows;
using System.Windows.Controls;

namespace View.Settings
{
    public class SettingFieldTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
        {
            if (item is not SettingFieldViewModel field || container is not FrameworkElement element)
                return base.SelectTemplate(item, container);

            var type = Nullable.GetUnderlyingType(field.PropertyType) ?? field.PropertyType;

            var key = type switch
            {
                _ when type.IsEnum => "EnumFieldTemplate",
                _ when type == typeof(bool) => "BoolFieldTemplate",
                _ when type == typeof(int) => "IntFieldTemplate",
                _ when type == typeof(double) => "DoubleFieldTemplate",
                _ => "StringFieldTemplate"
            };

            return (DataTemplate)element.FindResource(key);
        }
    }
}
