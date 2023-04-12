using System;
using _19X_Traceability_UWP.DAL;
using System.Data.SqlClient;
using Windows.Storage;

namespace _19X_Traceability_UWP.BL
{
    internal class FoldingKeyService
    {
        public async void ExportFoldingKeys(DateTime from, DateTime to, string connectedDriveName)
        {
            string fromString = from.ToString("dd.MM.yyyy");
            string toString = to.ToString("dd.MM.yyyy");
            
            string query = @"SELECT
                FK.id,
                dateTime,
                SN,
                SKP,
                custNr,
                keyCode,
                result AS resultId,
                text AS resultDescription,
                pcInSet,
                actCnt,
                home,
                ins,
                keyPress,
                spirollPress,
                takeOut,
                forceKey,
                forceSpiroll,
                notEnough,
                tooMuch,
                spirolDepth
                FROM FK LEFT OUTER JOIN alarmFK ON alarmFK.id = FK.result
                ORDER BY FK.id ASC;";

            DbHandler dbHandler = new DbHandler();
            SqlCommand sqlCmd = dbHandler.CreateSqlCmd(query);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@from", fromString);
            sqlCmd.Parameters.AddWithValue("@to", toString);

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(connectedDriveName);
                StorageFile file = await folder.CreateFileAsync("test.csv",
                    CreationCollisionOption.ReplaceExisting);
                
                string outputStr = String.Empty;

                object[] output = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                outputStr += string.Join(",", output) + "\n";

                while (reader.Read())
                {
                    reader.GetValues(output);
                    outputStr += string.Join(",", output) + "\n";
                }



                await FileIO.WriteTextAsync(file, outputStr);
                
                reader.Close();
            }

            dbHandler.CloseDbConn();
        }
    }
}
