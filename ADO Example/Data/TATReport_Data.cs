using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ADO_Example.Models;
using System.Configuration;

namespace ADO_Example.Data
{
    public class TATReport_Data
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //private readonly string _connectionString;

        public TATReport_Data(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TATReportModel> GetTATReports()
        {
            List<TATReportModel> tatReports = new List<TATReportModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTATReport", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tatReports.Add(new TATReportModel
                        {
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            ShowDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("dd-MM-yyyy"),
                            ClosingDate = reader["ClosingDate"] as DateTime?,
                            CloseDate = reader["ClosingDate"] != DBNull.Value ? Convert.ToDateTime(reader["ClosingDate"]).ToString("dd-MM-yyyy") : null,
                            TicketNumber = reader["TicketNumber"].ToString(),
                            Department = reader["Department"].ToString(),
                            Category = reader["Category"].ToString(),
                            Subcategory = reader["Subcategory"].ToString(),
                            Status = reader["Status"].ToString(),
                            TAT = reader["TAT"] as int?,// Updated to handle formatted TAT (e.g., "2 days" or "12 hours")
                            TATDate = reader["TAT"].ToString()
                        });
                    }
                }
            }

            return tatReports;
        }

    }
}