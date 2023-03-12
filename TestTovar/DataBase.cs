using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TestTovar
{
    public class DataBase : IDisposable
    {
        bool _isConnected;
        SqlConnection _connection;
        string _connectionString = @"Data Source=DESKTOP-AVGELME\STP;Initial Catalog=trade;Integrated Security=True";

        public DataBase()
        {
            ConnectionOpen();
        }

        public void ConnectionOpen()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _isConnected = true;
        }

        public void ConnectionClose()
        {
            _connection.Close();
            _isConnected = false;
        }

        public DataTable ExecuteSql(string sql)
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            dataTable.Load(reader);

            return dataTable;
        }

        public void ExecuteSqlNonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            ConnectionClose();
        }
    }
}
