using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReflectionHelper
{
    public static class DataSet_Extension
    {
        #region "public methods"
        public static IList<T> DataTable_To_List<T>(this IList<T> result, IList<ValuePair> Mapper,  DataTable data) where T : new()
        {
            foreach(DataRow row in data.Rows)
            {                
                result.Add(new T().DataRow_To_Object( Mapper, row, data.Columns));
            }
            return result;
        }
        #endregion

        #region "private methods"

        private static T DataRow_To_Object<T>(this T result, IList<ValuePair> Mapper, DataRow row, DataColumnCollection columns)
        {
            foreach (DataColumn column in columns)
            {
                ValuePair item = Mapper.IndexOf(column.ColumnName);
                if (item != null)
                {
                    result = result.Set_Property(item.Value, row.GetColumn(column.ColumnName));                    
                }
            }
            return result;
        }

        private static Object GetColumn(this DataRow row, string name)
        {
            return row[name];
        }

        private static T IndexOf<T>(this IList<T> data, string name) where T : new()
        {
            T result = default(T);
            int index = 0;
            foreach (T item in data)
            {
                if (item.Get_Property(name) != null)
                {
                    result = item;
                    break;
                }
                index++;
            }
            return result;
        }

        #endregion
    }
}
