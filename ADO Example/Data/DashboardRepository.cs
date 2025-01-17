using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ADO_Example.Models;

namespace ADO_Example.Data
{
    public class DashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public DashboardViewModel GetDashboardData()
        {
            var dashboardData = new DashboardViewModel();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDashboardData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dashboardData.OpenTickets = reader.GetInt32(0);
                            dashboardData.ClosedTickets = reader.GetInt32(1);
                            dashboardData.ResolvedTickets = reader.GetInt32(2);
                            dashboardData.TotalUsers = reader.GetInt32(3);
                        }
                    }
                }
            }

            return dashboardData;
        }
        public List<DepartmentTicketData> GetTicketsByDepartment()
        {
            var departmentData = new List<DepartmentTicketData>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetTicketsByDepartment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departmentData.Add(new DepartmentTicketData
                            {
                                DepartmentName = reader.GetString(0),
                                TicketCount = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }

            return departmentData;
        }

        public List<TicketStatusByDate> GetTicketsStatusByDate()
        {
            var statusData = new List<TicketStatusByDate>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetTicketsStatusByDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statusData.Add(new TicketStatusByDate
                            {
                                TicketDate = reader.GetDateTime(0),
                                OpenTickets = reader.GetInt32(1),
                                
                                ClosedTickets = reader.GetInt32(2),
                                ResolvedTickets = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return statusData;
        }
        public List<Ticket> GetOpenTickets()
        {
            List<Ticket> openTickets = new List<Ticket>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("GetOpenTickets", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    openTickets.Add(new Ticket
                    {
                        TicketNo = reader["TicketNo"].ToString(),
                        // Add other fields if needed
                    });
                }
            }

            return openTickets;
        }


    }
}