using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionHelper
{
    public static class ReflectionExtension
    {
        public static T Set_Property<T>(this T data, string name, Object value)
        {
            foreach (PropertyInfo property in data.GetType().GetProperties())
            {
                if (property.Name.ToLower() == name.ToLower())
                {
                    property.SetValue(data, Convert.ChangeType(value, property.PropertyType), null);
                }
            }
            return data;
        }

        public static Object Get_Property<T>(this T data, string name)
        {
            Object result = null;
            foreach (PropertyInfo property in data.GetType().GetProperties())
            {
                if (property.Name.ToLower() == name.ToLower())
                {
                    result = property.GetValue(data, null);
                }
            }
            return result;
        }
    }
}
