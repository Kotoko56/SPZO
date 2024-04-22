using Microsoft.Data.Sqlite;

namespace SPZO.DataManagement
{
    public class SQLiteDataAccess
    {
        private const string DBCONNECTION = "Data Source = database.db";

        public void InitializeDatabase() 
        { 
            using (SqliteConnection connection = new SqliteConnection(DBCONNECTION))
            {
                connection.Open();

                string sql_q1 = "CREATE TABLE IF NOT EXISTS Clients (Id INTEGER PRIMARY KEY, Name TEXT, Surname TEXT, PESEL TEXT, HomeAddress TEXT, VetNumer TEXT, PhoneNumer TEXT, RhdNumer TEXT, ArimrNumer TEXT)";

                using(SqliteCommand command = new SqliteCommand(sql_q1, connection)) 
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
