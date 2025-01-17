using System;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using ADO_Example.Data;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        // Check if the exception is not handled
        if (!filterContext.ExceptionHandled)
        {
            // Get the exception details
            var exception = filterContext.Exception;
            var errorMessage = exception.Message;
            var source = exception.Source;
            var stackTrace = exception.StackTrace;
            var errorType = exception.GetType().ToString();
            var createdBy = "System"; 

            // Log the exception to the database
            LogExceptionToDatabase(errorMessage, source, errorMessage, errorType, stackTrace, createdBy);

            // Mark the exception as handled
            filterContext.ExceptionHandled = true;
            // Redirect to the custom error page
            filterContext.Result = new RedirectResult("~/product/Error");
        }
    }

    private void LogExceptionToDatabase(string error, string source, string message, string errorType, string stackType, string createdBy)
    {
        // Database connection string
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        // SQL query to insert the exception into the LogException table
        string query = "EXEC InsertLogException @Error, @Source, @Message, @ErrorType, @StackType, @Created_By";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Error", error);
            command.Parameters.AddWithValue("@Source", source);
            command.Parameters.AddWithValue("@Message", message);
            command.Parameters.AddWithValue("@ErrorType", errorType);
            command.Parameters.AddWithValue("@StackType", stackType);
            command.Parameters.AddWithValue("@Created_By", createdBy);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
    

}
