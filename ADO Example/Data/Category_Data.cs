using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ADOExample.Models;
using ADO_Example.Models;

namespace ADOExample.Data
{
    public class Category_Data
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Get all categories from the database
        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return categories;
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



        // Insert category
        public void InsertCategory(string categoryName, string departmentName, string status, string createdIP)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Directly use the passed parameters
                cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreatedIP", createdIP);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt); // Executes the command
            }
        }


       

        // Delete category
        public void DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Category GetCategoryById(int id)
        {
            Category category = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCategoryById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    category = new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                }
            }
            return category;
        }
        public void UpdateCategory(CategoryEditViewModel model, string updatedIP)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryID", model.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", model.CategoryName);
                cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                cmd.Parameters.AddWithValue("@Status", model.Status);
                cmd.Parameters.AddWithValue("@UpdatedIP", updatedIP);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }





    }
}