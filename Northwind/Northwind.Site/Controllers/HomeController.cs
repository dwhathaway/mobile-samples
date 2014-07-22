using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using AIG_Common;
using System.Data;

namespace Northwind.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetProducts()
        {
            
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn))
                {
                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);

                    products = (from DataRow row in ds.Tables[0].Rows

                     select new Product
                     {
                         ProductName = row["ProductName"].ToString(),
                         SupplierID = int.Parse(row["SupplierID"].ToString()),
                         CategoryID = int.Parse(row["CategoryID"].ToString()),
                         QuantityPerUnit = row["QuantityPerUnit"].ToString(),
                         UnitPrice = double.Parse(row["UnitPrice"].ToString()),
                         UnitsInStock = int.Parse(row["UnitsInStock"].ToString()),
                         UnitsOnOrder = int.Parse(row["UnitsOnOrder"].ToString()),
                         ReorderLevel = int.Parse(row["ReorderLevel"].ToString()),
                         Discontinued = bool.Parse(row["Discontinued"].ToString())
                     }).ToList();
                }
            }

            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}