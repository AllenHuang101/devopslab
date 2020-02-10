using devopslabautotest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devopslabautotest.Controllers
{
    public class HomeController : Controller
    {

        public string queryEmployee(string name = "Allen")
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {


                string queryString = $"select * from Employee where Name ='{name}' ";


                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

                try
                {
                    connection.Open();
                    DataSet employees = new DataSet();
                    adapter.Fill(employees, "Employee");

                    List<Employee> employeeList = new List<Employee>();

                    foreach (DataRow row in employees.Tables["Employee"].Rows)
                    {
                        employeeList.Add(new Employee
                        {
                            Id = int.Parse(row["Id"].ToString()),
                            Name = row["Name"].ToString(),
                            Phone = row["Phone"].ToString(),
                            Dept = row["Dept"].ToString(),
                        });
                    }

                    return JsonConvert.SerializeObject(employeeList);

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }

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
    }
}