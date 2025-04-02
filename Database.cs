using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_tickets
{
    public class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=1337;database=railway_station");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) //Открываем соединение
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open) //Закрываем соединение
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }


        // Метод для выполнения sql-запроса с параметрами, возвращающего результат в виде таблицы
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null, MySqlTransaction transaction = null)
        {
            MySqlConnection connection = getConnection(); // Получаем соединение
            DataTable dataTable = new DataTable();

            try
            {
                openConnection();

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {
                    if (parameters != null) // Если переданы параметры, добавляем их
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            finally
            {
                if (transaction == null) // Закрываем соединение, если транзакции нет
                {
                    closeConnection();
                }
            }

            return dataTable;
        }


        // Метод для выполнения sql-запроса с параметрами, возвращающего результат в виде строки
        public string ExecuteScalarQuery(string query, Dictionary<string, object> parameters, MySqlTransaction transaction = null)
        {
            MySqlConnection connection = getConnection();

            try
            {
                openConnection();// Открываем соединение перед выполнением запроса

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {
                    // Добавляем параметры в команду
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : string.Empty;
                }
            }
            finally
            {
                if (transaction == null) // Закрываем соединение, если транзакции нет
                {
                    closeConnection();
                }
            }
        }

    }
}
