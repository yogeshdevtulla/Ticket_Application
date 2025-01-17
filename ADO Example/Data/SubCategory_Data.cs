using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ADO_Example.Models;
using ADOExample.Models;


namespace ADO_Example.Data
{

    public class SubCategory_Data
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<SubCategory> GetAllSubCategories()
        {
            var subCategories = new List<SubCategory>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllSubCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    subCategories.Add(new SubCategory
                    {
                        SubCategoryID = (int)reader["SubCategoryID"],
                        SubCategoryName = reader["SubCategoryName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return subCategories;
        }

        // Fetch all Categories using stored procedure
        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Correct the procedure name here
                SqlCommand cmd = new SqlCommand("GetAllCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
            return categories;
        }

        // Fetch all Departments using stored procedure
        public List<DepartmentModel> GetAllDepartments()
        {
            var department = new List<DepartmentModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Correct the procedure name here
                SqlCommand cmd = new SqlCommand("GetAllDepartments", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    department.Add(new DepartmentModel
                    {
                        Id = (int)reader["Id"],
                        DepartmentName = reader["DepartmentName"].ToString()
                    });
                }
            }
            return department;
        }

        // Create SubCategory using stored procedure
        public void InsertSubCategory(SubCategory subCategory)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertSubCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName);
                //cmd.Parameters.AddWithValue("@CategoryName", subCategory.);
                //cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                cmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                cmd.Parameters.AddWithValue("@DepartmentID", subCategory.Id);
                cmd.Parameters.AddWithValue("@Status", subCategory.Status);

                //cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedIP", "127.0.0.1"); // Replace with actual IP logic

                //cmd.Parameters.AddWithValue("@UpdatedDate", subCategory.UpdatedDate ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@UpdatedIP", string.IsNullOrEmpty(subCategory.UpdatedIP) ? (object)DBNull.Value : subCategory.UpdatedIP);


                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }


        public void DeleteSubCategory(int subCategoryId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteSubCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubCategoryID", subCategoryId);


                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Fetch a single subcategory by ID
        public SubCategory GetSubCategoryById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetSubCategoryById", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubCategoryID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new SubCategory
                    {
                        SubCategoryID = (int)reader["SubCategoryID"],
                        SubCategoryName = reader["SubCategoryName"].ToString(),
                        CategoryID = (int)reader["CategoryID"],
                        Id = (int)reader["DepartmentID"],
                        Status = reader["Status"].ToString()
                    };
                }
            }

            return null; // If no record is found
        }

        // Update an existing subcategory
        public void UpdateSubCategory(SubCategory subCategory)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateSubCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubCategoryID", subCategory.SubCategoryID);
                cmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName);
                cmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                cmd.Parameters.AddWithValue("@DepartmentID", subCategory.Id);
                cmd.Parameters.AddWithValue("@Status", subCategory.Status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }

}
