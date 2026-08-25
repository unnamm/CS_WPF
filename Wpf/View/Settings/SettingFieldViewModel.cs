using System;
using System.Reflection;

namespace View.Settings
{
    public class SettingFieldViewModel
    {
        readonly object _target;
        readonly PropertyInfo _property;

        public string DisplayName { get; }
        public Type PropertyType => _property.PropertyType;
        public Array? EnumValues => PropertyType.IsEnum ? Enum.GetValues(PropertyType) : null;

        public object? Value
        {
            get
            {
                var raw = _property.GetValue(_target);
                return IsIntegerType(PropertyType) && raw is not null ? Convert.ToDouble(raw) : raw;
            }

            set
            {
                if (value is null)
                {
                    _property.SetValue(_target, null);
                    return;
                }

                var targetType = Nullable.GetUnderlyingType(PropertyType) ?? PropertyType;

                object converted;
                if (targetType.IsInstanceOfType(value))
                    converted = value;
                else if (value is double d && IsIntegerType(targetType))
                    converted = Convert.ChangeType(Math.Floor(d), targetType);
                else
                    converted = Convert.ChangeType(value, targetType);

                _property.SetValue(_target, converted);
            }
        }

        public SettingFieldViewModel(object target, PropertyInfo property)
        {
            _target = target;
            _property = property;
            DisplayName = property.Name;
        }

        static bool IsIntegerType(Type type) =>
            type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(byte);
    }
}
