using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> PdtList = new List<Product>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDatabase;Integrated Security=True;";
            con.Open();
            SqlCommand listCmd = new SqlCommand();
            listCmd.Connection = con;
            //ListCmd.CommandType = System.Data.CommandType.StoredProcedure;
            listCmd.CommandType = System.Data.CommandType.Text;
            //ListCmd.CommandText = "ListAll";
            listCmd.CommandText = "select * from Products";

            SqlDataReader readData = listCmd.ExecuteReader();
            try
            {            
                while (readData.Read())
                {
                    PdtList.Add(new Product {ProductId=readData.GetInt32(0),  ProductName = readData.GetString(1), Rate = readData.GetDecimal(2), Description = readData.GetString(3), CategoryName = readData.GetString(4) });
                }
                readData.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }

            return View(PdtList);
        }

        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDatabase;Integrated Security=True;
        //

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Product> PdtList = new List<Product>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDatabase;Integrated Security=True;";
            con.Open();
            SqlCommand editCmd = new SqlCommand();
            editCmd.Connection = con;
            //editCmd.CommandType = System.Data.CommandType.StoredProcedure;
            editCmd.CommandType = System.Data.CommandType.Text;
            //editCmd.CommandText = "Edit";
            editCmd.CommandText = "select * from Products where ProductId=@Id";
            editCmd.Parameters.AddWithValue("@Id", id);

            Product p = null;

            SqlDataReader readData = editCmd.ExecuteReader();
            try
            {
                while (readData.Read())
                {
                    p=new Product { ProductId = readData.GetInt32(0), ProductName = readData.GetString(1), Rate = readData.GetDecimal(2), Description = readData.GetString(3), CategoryName = readData.GetString(4) };
                }
                readData.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return View(p);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Product o)
        {
            List<Product> PdtList = new List<Product>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ProductDatabase;Integrated Security=True;";
            con.Open();
            SqlCommand editCmd = new SqlCommand();
            editCmd.Connection = con;
            //editCmd.CommandType = System.Data.CommandType.StoredProcedure;
            editCmd.CommandType = System.Data.CommandType.Text;
            //editCmd.CommandText = "Edit";
            editCmd.CommandText = "update Products set ProductName = @ProductName, Rate = @Rate, Description = @Description, CategoryName = @CategoryName  where ProductId=@Id";
            editCmd.Parameters.AddWithValue("@Id", id);
            editCmd.Parameters.AddWithValue("@ProductName", o.ProductName);
            editCmd.Parameters.AddWithValue("@Rate", o.Rate);
            editCmd.Parameters.AddWithValue("@Description", o.Description);
            editCmd.Parameters.AddWithValue("@CategoryName", o.CategoryName);
            try
            {

                editCmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);   
            }
            return View();
        }

        
    }
}
