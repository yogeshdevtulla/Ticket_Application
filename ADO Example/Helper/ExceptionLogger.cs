//using System;
//using ADO_Example.Data;

//namespace ADO_Example.Helpers 
//{
//    public class ExceptionLogger
//    {

//        public static void LogException(Exception ex)
//        {
//            try
//            {
//                using (var dbContext = new YourDbContext()) /
//                {
//                    dbContext.Logs.Add(new LogEntry
//                    {
//                        ErrorMessage = ex.Message,
//                        StackTrace = ex.StackTrace,
//                        CreatedAt = DateTime.Now
//                    });

//                    dbContext.SaveChanges();
//                }
//            }
//            catch (Exception loggingEx)
//            {
//                // Handle logging failure (optional)
//                // You can log this to a file, event log, etc., to avoid recursive logging failures
//                Console.WriteLine($"Error logging exception: {loggingEx.Message}");
//            }
//        }
//    }
//}
