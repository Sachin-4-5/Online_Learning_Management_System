using System;
using System.Data;

namespace OnlineLearningManagementSystem.BLL.Helpers
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string column, T defaultValue = default)
        {
            if (!row.Table.Columns.Contains(column) || row[column] == DBNull.Value) return defaultValue;
            return (T)Convert.ChangeType(row[column], typeof(T));
        }
    }
}