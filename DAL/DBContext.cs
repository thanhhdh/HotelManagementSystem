using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DBContext
    {
        private SqlConnection conn = new SqlConnection("Server=(local);uid=sa;pwd=123456;database=HotelManagementSystem;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=True");

        public SqlConnection GetConnection()
        {
            return conn;
        }
        public void OpenConnect()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void CloseConnect()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}