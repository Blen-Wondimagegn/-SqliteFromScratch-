using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace SqliteFromScratch.Controllers {
   
    [Route("api/[Controller]")]
    public class EmployeeController : Controller {
        [HttpGet]
        public List<Employee> GetData()
        {

      List<Employee> employees = new List<Employee>();

     string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");
     using(SqliteConnection conn = new SqliteConnection(dataSource)) {

         conn.Open();
         string sql = $"select * from employees where HireDate < '2003-01-01 00:00:00'";
         
         using(SqliteCommand command = new SqliteCommand(sql, conn)) {

             using(SqliteDataReader reader = command.ExecuteReader()) {
     
                 while (reader.Read()) {

                     // map the data to the model.
                       Employee newEmployee = new Employee() {
                        Id = reader.GetInt32(0),
                        LastName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        Title = reader.GetString(3),
                        ReportsTo = reader.GetValue(4).ToString(),
                        BirthDate = reader.GetValue(5).ToString(),
                        HireDate = reader.GetValue(6).ToString(),
                        Address = reader.GetValue(7).ToString(),
                        City = reader.GetValue(8).ToString(),
                        State = reader.GetValue(9).ToString(),
                        Country = reader.GetValue(10).ToString(),
                        PostalCode = reader.GetValue(11).ToString(),
                        Phone = reader.GetValue(12).ToString(),
                        Fax = reader.GetValue(13).ToString(),
                        Email = reader.GetValue(14).ToString(),
                    };
                       employees.Add(newEmployee);
                 }
             }
         }
         conn.Close();
     }
     return employees;
    }
    }

    
}
