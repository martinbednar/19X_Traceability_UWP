using _19X_Traceability_UWP.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19X_Traceability_UWP.BL
{
    public class LogService
    {
        public enum ExportType
        {
            New = 1,
            Date = 2,
            All = 3
        }


        public async Task logExport(ExportType exportType, int? firstSK, int? lastSK, int? firstFK, int? lastFK, int? firstKK, int? lastKK)
        {
            string query = "INSERT INTO exportLog(exportType,firstFK,lastFK,firstKK,lastKK,firstSK,lastSK) VALUES (@exportType,@firstFK,@lastFK,@firstKK,@lastKK,@firstSK,@lastSK);";
            DbHandler dbHandler = new DbHandler();
            SqlCommand sqlCmd = dbHandler.CreateSqlCmd(query);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@exportType", exportType);
            sqlCmd.Parameters.AddWithValue("@firstFK", firstFK ?? SqlInt32.Null);
            sqlCmd.Parameters.AddWithValue("@lastFK", lastFK ?? SqlInt32.Null);
            sqlCmd.Parameters.AddWithValue("@firstKK", firstKK ?? SqlInt32.Null);
            sqlCmd.Parameters.AddWithValue("@lastKK", lastKK ?? SqlInt32.Null);
            sqlCmd.Parameters.AddWithValue("@firstSK", firstSK ?? SqlInt32.Null);
            sqlCmd.Parameters.AddWithValue("@lastSK", lastSK ?? SqlInt32.Null);

            try
            {
                await sqlCmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            dbHandler.CloseDbConn();
        }
    }
}
