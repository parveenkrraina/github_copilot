using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lab2.StarterCode
{
    public class LegacyService
    {
        private string connectionString = "Server=localhost;Database=MyDb;";
        
        public DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState. Open)
                    conn.Close();
            }
            
            return dt;
        }
        
        public void AddCustomer(string name, string email, string phone)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            
            try
            {
                conn.Open();
                string query = $"INSERT INTO Customers (Name, Email, Phone) VALUES ('{name}', '{email}', '{phone}')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // Swallowing exceptions
            }
            finally
            {
                conn.Close();
            }
        }
        
        public List<string> ProcessOrders(DataTable orders)
        {
            List<string> results = new List<string>();
            
            for (int i = 0; i < orders.Rows.Count; i++)
            {
                DataRow row = orders.Rows[i];
                
                if (row["Status"].ToString() == "Pending")
                {
                    if (Convert.ToDecimal(row["Total"]) > 0)
                    {
                        if (row["CustomerId"] != DBNull.Value)
                        {
                            string result = "Order " + row["Id"]. ToString() + " processed";
                            results.Add(result);
                        }
                    }
                }
            }
            
            return results;
        }
    }
}