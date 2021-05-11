using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient; //sqlconnection, sqlcommand, sqlreader will hv red underline cuz class not defined. thus add here then the below not nid one by one add
using web2020apr_p08_t5.Models;

namespace web2020apr_p08_t5.DAL
{
    public class FlightRouteDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        //Constructor         
        public FlightRouteDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json"); //fine app setting

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
                "FlightConnectionString"); //get data n key??

            //Instantiate a SqlConnection object with the
            //Connection String read.               
            conn = new SqlConnection(strConn); //then connect   
        }

        public List<FlightRoute> GetAllFlightRoute()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement              
            cmd.CommandText = @"SELECT * FROM FlightRoute ORDER BY RouteID";
            //Open a database connection             
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader(); //scroll row by row in 1 direction cannot reverse

            //Read all records until the end, save data into a staff list
            List<FlightRoute> FlightRouteList = new List<FlightRoute>();
            while (reader.Read())
            {
                FlightRouteList.Add(
                    new FlightRoute
                    {
                        RouteID = reader.GetInt32(0),    //0: 1st column
                        DepartureCity = reader.GetString(1),      //1: 2nd column
                        
                        DepartureCountry = reader.GetString(2), //2: 3rd column
                        ArrivalCity = reader.GetString(3),     //3: 4th column
                        ArrivalCountry = reader.GetString(4),   //5: 6th column 
                        FlightDuration = reader.GetInt32(5),  //6: 7th column
                        
                    }
                );
            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return FlightRouteList;
        }

        public int Add(FlightRoute FlightRoute)
        {
            //Create a SqlCommand object from connection object      
            SqlCommand cmd = conn.CreateCommand();

            //Specify an INSERT SQL statement which will      
            //return the auto-generated StaffID after insertion      
            cmd.CommandText = @"INSERT INTO FlightRoute (DepartureCity, DepartureCountry, ArrivalCity,
                                ArrivalCountry, FlightDuration)
                                OUTPUT INSERTED.RouteID
                                VALUES(@DepartureCity, @DepartureCountry, @ArrivalCity, @ArrivalCountry,
                                @FlightDuration)";
            //Define the parameters used in SQL statement, value for each parameter     
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@DepartureCity", FlightRoute.DepartureCity);
            cmd.Parameters.AddWithValue("@DepartureCountry", FlightRoute.DepartureCountry);
            cmd.Parameters.AddWithValue("@ArrivalCity", FlightRoute.ArrivalCity);
            cmd.Parameters.AddWithValue("@ArrivalCountry", FlightRoute.ArrivalCountry);
            cmd.Parameters.AddWithValue("@FlightDuration", FlightRoute.FlightDuration);
            

            //A connection to database must be opened before any operations made.     
            conn.Open();

            //ExecuteScalar is used to retrieve the auto-generated     
            //StaffID after executing the INSERT SQL statement     
            FlightRoute.RouteID = (int)cmd.ExecuteScalar();

            //A connection should be closed after operations.     
            conn.Close();

            //Return id when no error occurs.     
            return FlightRoute.RouteID;
        }
    }
}
