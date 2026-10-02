using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace StocklyMobi
{
    class DBConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        public string myConnection()
        {
            return ConfigurationManager
                   .ConnectionStrings["POSDB"]
                   .ConnectionString;
        }


        public DataTable getTable(string qury)
        {
            cn.ConnectionString = myConnection();
            cm = new SqlCommand(qury, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public void ExecuteQuery(String sql)
        {
            try
            {
                cn.ConnectionString = myConnection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public String getPassword(string username)
        {
            using (SqlConnection connection = new SqlConnection(myConnection()))
            using (SqlCommand command = new SqlCommand(
                "SELECT password FROM tbUser WHERE username = @username", connection))
            {
                command.Parameters.Add("@username", SqlDbType.VarChar, 50)
                    .Value = username ?? string.Empty;

                connection.Open();
                object result = command.ExecuteScalar();

                return result == null || result == DBNull.Value
                    ? string.Empty
                    : Convert.ToString(result);
            }
        }

        public double ExtractData(string sql)
        {
          
            cn = new SqlConnection();
            cn.ConnectionString = myConnection();
            cn.Open();
            cm = new SqlCommand(sql, cn);
            double data = double.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return data;

        }
    }
}
