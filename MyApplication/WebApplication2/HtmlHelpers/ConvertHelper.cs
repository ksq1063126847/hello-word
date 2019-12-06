using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication2.HtmlHelpers
{
    public class ConvertHelper
    {
        public static IEnumerable<T> GetList<T>(DataRow[] rows) where T : new()
        {
            List<T> result = new List<T>();
            if (rows != null && rows.Length > 0)
            {
                var columnColl = rows[0].Table.Columns;
                foreach (DataRow row in rows)
                {
                    var entity = new T();
                    foreach (var pinfo in typeof(T).GetProperties())
                    {
                        if (!columnColl.Contains(pinfo.Name) || row[pinfo.Name] == DBNull.Value)
                            continue;
                        pinfo.SetValue(entity, row[pinfo.Name], null);
                    }
                    result.Add(entity);
                }
            }
            return result;
        }

        public static IEnumerable<T> GetList<T>(DataTable table) where T : new()
        {
            List<T> result = new List<T>();
            if (table != null && table.Rows.Count > 0)
            {
                var columnColl = table.Columns;
                foreach (DataRow row in table.Rows)
                {
                    var entity = new T();
                    foreach (var pinfo in typeof(T).GetProperties())
                    {
                        //if (!columnColl.Contains(pinfo.Name) || pinfo.PropertyType != columnColl[pinfo.Name].DataType || row[pinfo.Name] == DBNull.Value)
                        //    continue;
                        if (!columnColl.Contains(pinfo.Name) || row[pinfo.Name] == DBNull.Value)
                            continue;
                        pinfo.SetValue(entity, row[pinfo.Name], null);
                    }
                    result.Add(entity);
                }
            }
            return result;
        }

        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SerializeDataTable(DataTable dt)
        {
            string strRet = "[]";
            if (dt == null || dt.Rows.Count == 0)
                return strRet;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (Newtonsoft.Json.JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                //jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;
                jsonWriter.WriteStartArray();
                foreach (DataRow dr in dt.Rows)
                {
                    jsonWriter.WriteStartObject();
                    foreach (DataColumn column in dt.Columns)
                    {
                        jsonWriter.WritePropertyName(column.ColumnName);
                        //if (dr[column] == DBNull.Value )
                        //    jsonWriter.WriteValue(DBNull.Value);
                        //else
                        jsonWriter.WriteValue(dr[column].ToString());
                    }
                    jsonWriter.WriteEndObject();
                }
                jsonWriter.WriteEndArray();
            }
            strRet = sb.ToString();
            sw.Close();
            return strRet;
        }

        public static string SearializeJson<T>(T entity) where T : new()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }
    }
}