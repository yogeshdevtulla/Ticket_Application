using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using ADO_Example.Models;

namespace ADO_Example.Data
{
    public class UserMaster_Data
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public List<UserMaster> GetAllUserMasterData()
        {
            var userList = new List<UserMaster>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetUserMasterData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nameParts = reader["Name"].ToString().Split(' ');
                            userList.Add(new UserMaster
                            {
                                UserID = (int)reader["UserID"],
                                FirstName = nameParts.Length > 0 ? nameParts[0] : string.Empty,
                                LastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : string.Empty,
                                DepartmentName = reader["DepartmentName"].ToString(),
                                MobileNo = reader["MobileNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                Status = reader["Status"].ToString(),
                                Role = string.Empty, // Placeholder, add if needed
                                Designation = string.Empty // Placeholder, add if needed
                            });
                        }

                    }
                }

                return userList;
            }
        }

        //Add new user

        public void AddNewUser(UserMaster user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("INSERT INTO UserMaster (FirstName, MiddleName, LastName, Role, Password, DepartmentID, Designation, MobileNo, Email, Status) VALUES (@FirstName, @MiddleName, @LastName, @Role, @Password, @DepartmentID, @Designation, @MobileNo, @Email, @Status)", connection))
                {
                    command.CommandType = CommandType.Text;

                    // Add parameters for the insert query
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    //command.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                    //command.Parameters.AddWithValue("@MiddleName", string.IsNullOrEmpty(user.MiddleName) ? (object)DBNull.Value : user.MiddleName);
                    command.Parameters.AddWithValue("@MiddleName", string.IsNullOrEmpty(user.MiddleName) ? "BSL" : user.MiddleName);

                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Password", user.Password);  // Password will be handled in trigger
                    command.Parameters.AddWithValue("@DepartmentID", user.DepartmentID); // DepartmentID
                    command.Parameters.AddWithValue("@Designation", user.Designation);
                    command.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Status", user.Status);

                    connection.Open();
                    command.ExecuteNonQuery();  // Insert into UserMaster, trigger will handle the Users table
                }
            }
        }


        public UserMaster GetUserMasterDataById(int id)
        {
            UserMaster user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetUserMasterDataById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", id);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserMaster
                            {
                                UserID = (int)reader["UserID"],
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Role = reader["Role"].ToString(),
                                Password = reader["Password"].ToString(),
                                DepartmentID = (int)reader["DepartmentID"],
                                Designation = reader["Designation"].ToString(),
                                MobileNo = reader["MobileNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }
        public bool EditUserMaster(UserMaster model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("EditUserMaster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", model.UserID);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", model.MiddleName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@Role", model.Role);
                    command.Parameters.AddWithValue("@Password", model.Password);  // Handle password separately if needed
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@Designation", model.Designation);
                    command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Status", model.Status);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if the update was successful
                }
            }
        }

        //dlt

        public bool DeleteUser(int userID)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("DeleteUserMaster", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging library like NLog, log4net, etc.)
                Console.WriteLine("Error occurred while deleting user: " + ex.Message);
                return false; // Indicating failure
            }
        }




        //delete

        //public bool DeleteUser(int userID)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            connection.Open();
        //            var command = new SqlCommand("DeleteUserMaster", connection);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            var rowsAffected = command.ExecuteNonQuery();
        //            return rowsAffected > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception as needed
        //        throw new Exception("Error in deleting user", ex);
        //    }
        //}






    }
}