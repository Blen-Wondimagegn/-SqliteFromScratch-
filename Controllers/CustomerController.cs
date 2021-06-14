using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace SqliteFromScratch.Controllers {
    // MVC is handling the routing for you.
    [Route("api/[Controller]")]
    
    public class CustomerController : Controller {

        [HttpGet]
     
    
        public List<Customer> GetData()
        {

            // tracks will be populated with the result of the query.
     List<Customer> customers = new List<Customer>();

     // GetFullPath will complete the path for the file named passed in as a string.
     string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");
     using(SqliteConnection conn = new SqliteConnection(dataSource)) {

         conn.Open();

         // sql is the string that will be run as an sql command
         string sql = $"select * from customers limit 20;";

         // command combines the connection and the command string and creates the query
         using(SqliteCommand command = new SqliteCommand(sql, conn)) {

             using(SqliteDataReader reader = command.ExecuteReader()) {
                 while (reader.Read()) {
                       Customer newCustomer = new Customer() {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Company = reader.GetValue(3).ToString(),
                        Address = reader.GetValue(4).ToString(),
                        City = reader.GetValue(5).ToString(),
                        State = reader.GetValue(6).ToString(),
                        Country = reader.GetValue(7).ToString(),
                        PostalCode = reader.GetValue(8).ToString(),
                        Phone = reader.GetValue(9).ToString(),
                        Fax = reader.GetValue(10).ToString(),
                        Email = reader.GetValue(11).ToString(),
                        SupportRepId  = reader.GetInt32(12)
                      
                    };
                       customers.Add(newCustomer);
                 }
             }
         }
         conn.Close();
     }
     return customers;
    }
    }
}
