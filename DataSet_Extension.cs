using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        public static DataTable List_To_DataTable<T>(this IList<T> data) where T : new()
        {
            T item = new T();
            DataTable result = new DataTable();
            result = result.Create_DataColumns(item);
            result = result.Create_DataRow(data);
            return result;
        }

        #endregion

        #region "private methods"

        private static DataTable Create_DataRow<T>(this DataTable result, IList<T> data)
        {
            foreach (T item in data)
            {
                DataRow row = result.NewRow();
                foreach(DataColumn column in result.Columns)
                {   
                    row[column.ColumnName] = item.Get_Property_Value(column.ColumnName);                    
                }
                result.Rows.Add(row);
            }
            return result;
        }

        private static DataTable Create_DataColumns<T>(this DataTable result, T data)
        {
            foreach (PropertyInfo property in data.Get_Properties())
            {
                result.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }
            return result;
        }

        private static T DataRow_To_Object<T>(this T result, IList<ValuePair> Mapper, DataRow row, ICollection columns)
        {
            foreach (DataColumn column in columns)
            {
                ValuePair item = Mapper.IndexOf(column.ColumnName);
                if (item.Has_Result())
                {
                    result = result.Set_Property_Value(item.Value, row.GetColumn(column.ColumnName));                    
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
            foreach (T item in data)
            {
                if (item.Get_Property_Value(name).Has_Result())
                {
                    result = item;
                    break;
                }            
            }
            return result;
        }

        private static bool Has_Result<T>(this T item)
        {
            bool result = false;
            result = item != null;
            return result;
        }
        #endregion
    }
}
