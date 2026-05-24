using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace ConcessionariaAPI.Repository
{
    public class Database
    {
        protected readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        protected int Execute(string sql, object obj)
        {
            using (MySqlConnection conexao = GetConnection())
            {
                return conexao.Execute(sql, obj);
            }
        }
    }
}