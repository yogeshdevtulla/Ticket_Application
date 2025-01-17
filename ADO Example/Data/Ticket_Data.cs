using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ADO_Example.Models;
using ADOExample.Models;
using ADOExample.Data;

namespace ADO_Example.Data
{
    public class Ticket_Data
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public void AddTicket(Ticket ticket)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddTicket", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentID", ticket.DepartmentID);
                cmd.Parameters.AddWithValue("@CategoryID", ticket.CategoryID);
                cmd.Parameters.AddWithValue("@SubCategoryID", ticket.SubCategoryID);
                cmd.Parameters.AddWithValue("@Priority", ticket.Priority);
                cmd.Parameters.AddWithValue("@Feedback", ticket.Feedback);
                cmd.Parameters.AddWithValue("@CreateIP", ticket.CreateIP);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // View tickets

        public List<TicketGridModel> GetTickets()
        {
            List<TicketGridModel> tickets = new List<TicketGridModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTickets", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tickets.Add(new TicketGridModel
                    {
                        CreateDate = Convert.ToDateTime(reader["CreateDate"]),                        
                        ShowDate = Convert.ToDateTime(reader["CreateDate"]).ToString("dd-MM-yyyy"),                        
                        TicketNo = reader["TicketNo"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        SubCategoryName = reader["SubCategoryName"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return tickets;
        }



        //edit
        // In Ticket_Data.cs

        public TicketUpdateModel GetTicketByTicketNo(string ticketNo)
        {
            TicketUpdateModel ticket = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetTicketByTicketNo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TicketNo", ticketNo);

                        con.Open();

                        // Using 'using' for SqlDataReader to ensure it gets disposed automatically
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ticket = new TicketUpdateModel
                                {
                                    TicketNo = reader["TicketNo"] as string,  // Using 'as' to handle NULL values
                                    //CreateDate = reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreateDate"]) : DateTime.MinValue,
                                    CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                                    ShowDate = Convert.ToDateTime(reader["CreateDate"]).ToString("dd-MM-yyyy"),
                                    //CreateDate = reader["CreateDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreateDate"]) : (DateTime?)null,
                                    DepartmentName = reader["DepartmentName"] as string,
                                    CategoryName = reader["CategoryName"] as string,
                                    SubCategoryName = reader["SubCategoryName"] as string,
                                    Status = reader["Status"] as string,
                                    Feedback = reader["Feedback"] as string
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                // For example: log.Error("Error fetching ticket details: " + ex.Message);
                throw new Exception("An error occurred while fetching the ticket details.", ex);
            }

            return ticket;  // Returns null if no ticket is found or an error occurs
        }

        public bool UpdateTicketStatus(TicketUpdateModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("UpdateTicketStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TicketNo", model.TicketNo);
                    cmd.Parameters.AddWithValue("@Status", model.Action);
                    //cmd.Parameters.AddWithValue("@Feedback", model.Feedback);
                    cmd.Parameters.AddWithValue("@Feedback", string.IsNullOrEmpty(model.Remarks) ? "No remarks provided" : model.Remarks);


                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;  // Return true if the update was successful
                }
            }
        }




    }
} 