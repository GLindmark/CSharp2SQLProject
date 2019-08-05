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

            var sql = "Select * from Customers;";
            var cmd = new SqlCommand(sql, connection);




            connection.Close();//this is always at the end so as to close the connection.  Connections are very resource intensive.
            
        }
    }
}
