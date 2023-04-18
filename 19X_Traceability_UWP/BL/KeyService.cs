using _19X_Traceability_UWP.DAL;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace _19X_Traceability_UWP.BL
{
    internal abstract class KeyService
    {
        protected abstract string SelectQuery { get; }

        protected abstract string OrderByQuery { get; }

        protected abstract string OutputFileName { get; }


        public async void ExportLast(string connectedDriveName)
        {
            DbHandler dbHandler = new DbHandler();
            SqlCommand sqlCmd = CreateSqlCmd(dbHandler, SelectQuery + OrderByQuery);
            await Export(sqlCmd, connectedDriveName);
            dbHandler.CloseDbConn();
        }

        public async void ExportDate(DateTime from, DateTime to, string connectedDriveName)
        {
            DbHandler dbHandler = new DbHandler();
            string whereQuery = " WHERE (cast(dateTime as DateTime) BETWEEN @from AND @to)";
            SqlCommand sqlCmd = CreateSqlCmd(dbHandler, SelectQuery + whereQuery + OrderByQuery);
            string fromString = from.ToString("yyyy-MM-dd");
            string toString = to.ToString("yyyy-MM-dd");
            SetSqlParameters(sqlCmd, fromString, toString);
            await Export(sqlCmd, connectedDriveName);
            dbHandler.CloseDbConn();
        }

        public async void ExportAll(string connectedDriveName)
        {
            DbHandler dbHandler = new DbHandler();
            SqlCommand sqlCmd = CreateSqlCmd(dbHandler, SelectQuery + OrderByQuery);
            await Export(sqlCmd, connectedDriveName);
            dbHandler.CloseDbConn();
        }

        private SqlCommand CreateSqlCmd(DbHandler dbHandler, string sqlQuery)
        {
            return dbHandler.CreateSqlCmd(sqlQuery);
        }

        private void SetSqlParameters(SqlCommand sqlCmd, string from, string to)
        {
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@from", from);
            sqlCmd.Parameters.AddWithValue("@to", to);
        }

        private async Task Export(SqlCommand sqlCmd, string connectedDriveName)
        {
            using (SqlDataReader reader = await sqlCmd.ExecuteReaderAsync())
            {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(connectedDriveName);
                StorageFile file = await folder.CreateFileAsync(DateTime.Now.ToString("yyyy_MM_dd") + "_" + OutputFileName + ".csv",
                    CreationCollisionOption.ReplaceExisting);

                string outputStr = String.Empty;

                object[] output = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                outputStr += string.Join(";", output) + "\n";

                while (reader.Read())
                {
                    reader.GetValues(output);
                    outputStr += string.Join(";", output) + "\n";
                }

                await FileIO.WriteTextAsync(file, outputStr);

                reader.Close();
            }
        }
    }
}
