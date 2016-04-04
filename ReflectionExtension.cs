using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionHelper
{
    public static class ReflectionExtension
    {
        #region "public extension"

        public static T Set_Property_Value<T>(this T result, string name, Object value)
        {
            PropertyInfo property = result.Get_Properties().Find_Property_By_Name(name);
            try
            {
                property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
            }
            catch (Exception)
            {

            }
            return result;
        }

        public static Object Get_Property_Value<T>(this T data, string name)
        {
            Object result = null;
            result = data.Get_Properties().Find_Property_By_Name(name).GetValue(data, null);            
            return result;
        }

        #endregion

        #region "private helper methods extension"

        internal static IList<PropertyInfo> Get_Properties<T>(this T result)
        {
            return result.GetType().GetProperties();
        }

        private static PropertyInfo Find_Property_By_Name(this IList<PropertyInfo> data, string name)
        {
            PropertyInfo result = null;
            foreach (PropertyInfo property in data)
            {
                if (property.Name.ToLower() == name.ToLower())
                {
                    result = property;
                    break;
                }
            }
            return result;
        }

        #endregion
    }
}
