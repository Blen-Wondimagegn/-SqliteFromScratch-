using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace SqliteFromScratch.Controllers {
    // MVC is handling the routing for you.
    [Route("api/[Controller]")]
    public class DatabaseController : Controller {

        // api/database
        [HttpGet]
        public List<Track> GetData()
        {

            // tracks will be populated with the result of the query.
     List<Track> tracks = new List<Track>();

     // GetFullPath will complete the path for the file named passed in as a string.
     string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");

     // using will make sure that the resource is cleaned from memory after it exists
     // conn initializes the connection to the .db file.
     using(SqliteConnection conn = new SqliteConnection(dataSource)) {

         conn.Open();

         // sql is the string that will be run as an sql command
         string sql = $"select * from tracks limit 200;";
         
         using(SqliteCommand command = new SqliteCommand(sql, conn)) {

             using(SqliteDataReader reader = command.ExecuteReader()) {
     
                 while (reader.Read()) {

                     // map the data to the model.
                       Track newTrack = new Track() {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        AlbumId = reader.GetInt32(2),
                        MediaTypeId = reader.GetInt32(3),
                        GenreId = reader.GetInt32(4),
                        Composer = reader.GetValue(5).ToString(),
                        Milliseconds = reader.GetInt32(6),
                        Bytes = reader.GetInt32(7),
                        UnitPrice = reader.GetInt32(8)
                    };
                       tracks.Add(newTrack);
                     // add each one to the list.
                 }
             }
         }
         conn.Close();
     }
     return tracks;
    }
    }

    
}
