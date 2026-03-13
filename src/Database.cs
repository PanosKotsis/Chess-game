using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Chess
{
    internal class Database
    {
        private readonly string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Δημιουργία βάσης δεδομένων
        public void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                String createTable = @"CREATE TABLE IF NOT EXISTS Chess (
	                player1 TEXT,
	                player2 TEXT,
                    winner TEXT,
	                time TEXT,
	                date TEXT
                );";

                using (SQLiteCommand command = new SQLiteCommand(createTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        // Αποθήκευση παιχνιδιού
        public void InsertRow(string player1, string player2, string winner, string time, string date)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insert = "Insert INTO CHESS (Player1, Player2, Winner, Time, Date) " +
                    "VALUES (@player1, @player2, @winner, @time, @date)";

                using (SQLiteCommand command = new SQLiteCommand(insert, connection))
                {
                    command.Parameters.AddWithValue("@player1", player1);
                    command.Parameters.AddWithValue("@player2", player2);
                    command.Parameters.AddWithValue("@winner", winner);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@date", date);

                    int rows = command.ExecuteNonQuery();
                    MessageBox.Show($"Rows inserted : {rows}");
                }

                connection.Close();
            }
        }
    }
}
