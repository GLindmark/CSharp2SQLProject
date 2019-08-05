using System;
using System.Data.SqlClient;

namespace CSharp2SQLProject {
    class Program {
        static void Main(string[] args) {
            var connStr = @"Server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
//above:
// use the @ sign so you can use just 1 '\' instead of 2 '\\'.  the @ means use the text string as entered
//'localhost' is the server 'sqlexpress' is the instance
//database is CustomerOrderDb
//trusted is true
            var connection = new SqlConnection(connStr);//instantiates the SQL Connection to "connStr"
            connection.Open();//this is to open the connection.  You MUST then test the connection by Opening it.
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("connection did not open!");

                //Above: it tells the state of the connection at any given time.  The != stands for "not equal".  
                //the throw informs that the connection is not OPEN

            }

            var sql = "Select * from Customers Where State = 'OH';";
            var cmd = new SqlCommand(sql, connection);//Command to include the Select Statement and the connection
            var reader = cmd.ExecuteReader();//var reader represents the Results Set which reviews each row and column
            while (reader.Read()) {
                var id = (int)reader["Id"];
                //tells it to read the Id column.  CAST the result to an '(int)' so that is returns an int, not an object
                var name = reader["Name"].ToString();//add the "ToString" so that it returns a string instead of an object
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                    ? null
                    : reader["Code"].ToString();

                Console.WriteLine($"Customer: {name}");
            }
            

                //Ternary Operator: the IsDBNull is bool;  "GetOrdinal"

                Console.WriteLine("Done...");
                reader.Close();
            





            connection.Close();//this is always at the end so as to close the connection.  Connections are very resource intensive.
            
        }
    }
}
