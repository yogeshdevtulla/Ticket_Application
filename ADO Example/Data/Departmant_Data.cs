using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ADO_Example.Models;
using System.Configuration;



namespace ADO_Example.Data
{
    public class Department_Data
    {
        private readonly string connectionString;

        public Department_Data()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }

        public List<DepartmentModel> GetAllDepartments()
        {
            List<DepartmentModel> departments = new List<DepartmentModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllDepartments", con);


                cmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    departments.Add(new DepartmentModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        DepartmentName = row["DepartmentName"].ToString(),
                        Status = row["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        CreatedIP = row["CreatedIP"].ToString(),
                        UpdatedDate = row["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UpdatedDate"]),
                        UpdatedIP = row["UpdatedIP"] == DBNull.Value ? null : row["UpdatedIP"].ToString()
                    });
                }
            }
            return departments;
        }
     
        public void InsertDepartment(string departmentName, string status, string createdIP)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertDepartment", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@CreatedIP", createdIP);

                    con.Open();
                    cmd.ExecuteNonQuery(); // Execute the command
                }
            }
            catch (Exception ex)
            {
                // Log the exception to the database
                throw new Exception("Error while inserting department: " + ex.Message, ex);
            }
        }


        public void UpdateDepartment(int id, string departmentName, string status, string updatedIP)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateDepartment", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@UpdatedIP", updatedIP);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt); // Executes the command
            }
        }

        public void DeleteDepartment(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteDepartment", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public DepartmentModel GetDepartmentById(int id)
        {
            DepartmentModel department = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDepartmentById", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            DepartmentName = reader["DepartmentName"].ToString(),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }

            return department;
        }
    }
}
