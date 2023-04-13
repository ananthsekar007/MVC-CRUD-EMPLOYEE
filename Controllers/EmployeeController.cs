using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVCProj.Models;
using NuGet.Protocol.Core.Types;
using System.Data;

namespace MVCProj.Controllers
{
    public class EmployeeController : Controller
    {
        IConfiguration _configuration;
        SqlConnection _Connection;
        public EmployeeController(IConfiguration configuration) {
            _configuration = configuration;
            _Connection = new SqlConnection(_configuration.GetConnectionString("Employee"));
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> Employees = new();
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("FETCH_EMPLOYEES", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                EmployeeModel employee = new();
                employee.Id = (int)reader["id"];
                employee.Name = (string)reader["name"];
                employee.Department = (string)reader["department"];
                employee.Salary = (decimal)reader["salary"];
                employee.Dob = (DateTime)reader["dob"];
                employee.BaseLocation = (string)reader["base_location"];
                Employees.Add(employee);
            }

            reader.Close();
            _Connection.Close();

            return Employees;
        }

        EmployeeModel GetEmployee(int id)
        {
            _Connection.Open();
            SqlCommand cmd = new SqlCommand("GET_EMPLOYEE", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpID", id);

            SqlDataReader reader = cmd.ExecuteReader();

            EmployeeModel employee = new();

            while (reader.Read())
            {

                employee.Id = (int)reader["id"];
                employee.Name = (string)reader["name"];
                employee.Department = (string)reader["department"];
                employee.Salary = (decimal)reader["salary"];
                employee.Dob = (DateTime)reader["dob"];
                employee.BaseLocation = (string)reader["base_location"];
            }
            return employee;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            return View(GetEmployees());
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View(GetEmployee(id));
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
