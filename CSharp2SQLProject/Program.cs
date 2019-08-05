using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp2SQLProject {
    class Program {
        static void Main(string[] args) {

            var sql = "Select * from Orders;";


            var orders = SelectOrder(sql);
            foreach(var order in orders) {

                Console.WriteLine($"Order Id: {order.Id}| {order.Date}");
            }

            /*
            var customers = SelectCustomer(sql);
            foreach(var customer in customers) {

                Console.WriteLine(customer.Name);
            */
            

        }//add closing curly brace to end the Main

        private static List<Order> SelectOrder(string sql) {

            var connStr = @"Server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";

            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("connection did not open!");
                
            }
                                   
            var orderList = new List<Order>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
                                                            
            while (reader.Read()) {
            var id = (int)reader["Id"];
            var date = (DateTime)reader["Date"];
            var note = reader["Note"].ToString();
            var customer = reader["CustomerId"].ToString();




             var order = new Order(id, date, note, customer);

             orderList.Add(order);

          
        }


        //static List<Customer> SelectCustomer(string sql) { //allows one to manipulate the Main Statement above while using all code below




            //var connStr = @"Server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
//above:
// use the @ sign so you can use just 1 '\' instead of 2 '\\'.  the @ means use the text string as entered
//'localhost' is the server 'sqlexpress' is the instance
//database is CustomerOrderDb
//trusted is true
/*
            var connection = new SqlConnection(connStr);//instantiates the SQL Connection to "connStr"
            connection.Open();//this is to open the connection.  You MUST then test the connection by Opening it.
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("connection did not open!");
                
                //Above: it tells the state of the connection at any given time.  The != stands for "not equal".  
                //the throw informs that the connection is not OPEN

            }

            var customerList = new List<Customer>();

            //var sql = "Select * from Customers Where State = 'OH';";  we removed this when we separated out the "Main"above
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
                // Above: Ternary Operator: the IsDBNull is bool;  it asks, is the code null? If yes, put 'null' in the results.  
                //if the code has a value, ' set it to the value in the reader (GetOrdinal).

                var customer = new Customer(id, name, city, state, active, code);
                customerList.Add(customer);//add customer to the class

                //Console.WriteLine($"Customer: {name}");
            }


            
    */
             

                //Console.WriteLine("Done...");
                reader.Close();
            
    




            connection.Close();//this is always at the end so as to close the connection.  Connections are very resource intensive.

           // return customerList;

            return orderList;
            
        }
    }
}
