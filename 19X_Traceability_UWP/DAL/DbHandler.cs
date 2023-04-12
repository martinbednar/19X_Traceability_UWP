using System.Data.SqlClient;

namespace _19X_Traceability_UWP.DAL
{
    internal class DbHandler
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=19X;user id=dacia;password=PrestrojenaDacia19X";

        private SqlConnection _dbConnection;

        public SqlCommand CreateSqlCmd(string query)
        {
            SqlCommand sqlCmd = new SqlCommand(query);

            _dbConnection = new SqlConnection(_connectionString);
            _dbConnection.Open();
            sqlCmd.Connection = _dbConnection;

            return sqlCmd;
        }

        public void CloseDbConn()
        {
            _dbConnection.Close();
        }
    }
}
