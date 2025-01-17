using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ADO_Example.Data;
using ADO_Example.Models;

public class TATReportController : Controller
{
    private readonly TATReport_Data _tatReportData;

    public TATReportController()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        _tatReportData = new TATReport_Data(connectionString);
    }

    // Index action
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public JsonResult GetTATReportData()
    {
        try
        {
            List<TATReportModel> tatReports = _tatReportData.GetTATReports();
            return Json(tatReports, JsonRequestBehavior.AllowGet);
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL Error: " + sqlEx.Message);
            return Json(new { success = false, message = "A database error occurred. Please try again later." });
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return Json(new { success = false, message = "An error occurred. Please try again later." });
            throw;
        }
    }
}