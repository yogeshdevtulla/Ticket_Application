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
    public class RoleMasterData
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public List<RoleMaster> GetRoles()
        {
            var roles = new List<RoleMaster>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllRoles", conn) 
                { CommandType = CommandType.StoredProcedure };
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new RoleMaster
                        {
                            RoleID = (int)reader["RoleID"],
                            RoleName = reader["RoleName"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }
            return roles;
        }

        public void AddNewRole(RoleMaster role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("INSERT INTO RoleMaster (RoleName, Status) VALUES (@RoleName, @Status)", connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add parameters for the insert query
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@Status", role.Status);

                    connection.Open();
                    command.ExecuteNonQuery();  // Insert into RoleMaster table
                }
            }
        }
        

        public RoleMaster GetRoleById(int id)
        {
            RoleMaster role = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRoleById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        role = new RoleMaster
                        {
                            RoleID = Convert.ToInt32(reader["RoleID"]),
                            RoleName = reader["RoleName"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }
            return role;
        }

        public bool UpdateRole(RoleMaster role)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateRole", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", role.RoleID);
                    cmd.Parameters.AddWithValue("@RoleName", role.RoleName);
                    cmd.Parameters.AddWithValue("@Status", role.Status);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }


        //

        public bool DeleteRole(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DeleteRoleMaster", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@RoleID", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        

    }
}