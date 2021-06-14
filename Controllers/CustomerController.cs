using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace SqliteFromScratch.Controllers {
    // MVC is handling the routing for you.
    [Route("api/[Controller]")]
    
    public class CustomerContoller : Controller {

        [HttpGet]
        public List<Customer> GetData()
        {

            // tracks will be populated with the result of the query.
     List<Customer> customers = new List<Customer>();

     // GetFullPath will complete the path for the file named passed in as a string.
     string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");

     // using will make sure that the resource is cleaned from memory after it exists
     // conn initializes the connection to the .db file.
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
                        LastName = reader.GetString(1),
                        Company = reader.GetString(1),
                        Address = reader.GetString(1),
                        City = reader.GetString(1),
                        State = reader.GetString(1),
                        Country = reader.GetString(1),
                        PostalCode = reader.GetString(1),
                        Phone = reader.GetString(1),
                        Fax = reader.GetString(1),
                        Email = reader.GetString(1),
                        SupportRepId  = reader.GetInt32(7)
                      
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
