using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SFTP
{
    public class CSVHelper
    {
        /// <summary>
        /// Provide the functionality to Convert csv string to datatable
        /// </summary>
        /// <param name="csvContent"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public DataTable ConvertCSVToTable(string csvContent, int startRow)
        {
            string csv = csvContent;
            DataTable table = new DataTable();
            string[] lines = csv.Split('\n');
            int columnIndex = 0, rowIndex = 0;

            string[] columns = null;
            if (lines[0].Contains("Textbox"))
            {
                columns = lines[1].Trim(' ', '\t', '\n', '\v', '\f', '\r', '"').Split(',');
                startRow++; ;
            }
            else
            {
                columns = lines[0].Trim(' ', '\t', '\n', '\v', '\f', '\r', '"').Split(',');
            }

            for (columnIndex = 0; columnIndex < columns.Length; columnIndex++)
            {
                table.Columns.Add(columns[columnIndex].ToString());
            }
            DataRow newRow;
            for (rowIndex = startRow - 1; rowIndex < lines.Length; rowIndex++)
            {
                newRow = table.NewRow();
                columns = lines[rowIndex].Trim(' ', '\t', '\n', '\v', '\f', '\r', '"').Split(',');
                for (columnIndex = 0; columnIndex < columns.Length; columnIndex++)
                {
                    if (!string.IsNullOrEmpty(columns[columnIndex]) && columns[columnIndex] != "NaN")
                    {
                        newRow[columnIndex] = columns[columnIndex];
                    }
                }
                table.Rows.Add(newRow);
            }
            return table;
        }

        /// <summary>
        /// Provide the functionality to convert datatable to csv string
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string ConvertTableToCSVString(DataTable table)
        {
            StringBuilder csvResult = new StringBuilder("");
            int columnIndex = 0, maxColumnCount = table.Columns.Count;
            foreach (DataRow row in table.Rows)
            {
                for (columnIndex = 0; columnIndex < maxColumnCount; columnIndex++)
                {
                    if (columnIndex == 0)
                    {
                        csvResult.Append(row[columnIndex]);
                    }
                    else
                    {
                        if (row[columnIndex] == DBNull.Value)
                        {
                            csvResult.Append(",");
                            csvResult.Append("0");
                        }
                        else
                        {
                            csvResult.Append(",");
                            csvResult.Append(row[columnIndex]);
                        }
                    }
                }
                csvResult.AppendLine();
            }

            csvResult.Remove(csvResult.Length - 1, 1);
            return csvResult.ToString();
        }

        public void ToCSV(DataSet ds, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    sw.Write(ds.Tables[0].Columns[i]);
                    if (i < ds.Tables[0].Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (System.Data.DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            if (!Convert.IsDBNull(dr[i]))
                            {
                                string value = dr[i].ToString();
                                if (value.Contains(','))
                                {
                                    value = String.Format("\"{0}\"", value);
                                    sw.Write(value);
                                }
                                else
                                {
                                    sw.Write(dr[i].ToString());
                                }
                            }
                            if (i < table.Columns.Count - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.Write(sw.NewLine);
                    }
                }


            }

            sw.Close();
        }

        public DataTable ToDataTable<T>(List<T> items, string country = null)
        {
            DataTable dataTable = null;
            if (country != null)
            {
                dataTable = new DataTable(country);
            }
            else
            {
                dataTable = new DataTable();
            }
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
