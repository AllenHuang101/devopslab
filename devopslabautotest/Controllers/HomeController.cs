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
        public void injectionSample(string name, string pass)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=MyDb;Integrated Security=True");
            string qry = "select * from MyTable where Email='" + name + "'and Password='" + pass + "' ";
            SqlDataAdapter adpt = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                Response.Redirect("index.aspx");
            }
             
        }
        public void Bar(SqlConnection connection)
        {
            SqlCommand command;
            string param = "test";
            string sensitiveQuery = string.Format("INSERT INTO Users (name) VALUES (\"{0}\")", param);
            command = new SqlCommand(sensitiveQuery); // Sensitive

            command.CommandText = sensitiveQuery; // Sensitive

            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(sensitiveQuery, connection); // Sensitive
        }

        public string queryEmployee(string name = "Allen")
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {


                string queryString = string.Format("select * from Employee where Name ={0}",name);


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