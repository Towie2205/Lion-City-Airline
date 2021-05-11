using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient; //sqlconnection, sqlcommand, sqlreader will hv red underline cuz class not defined. thus add here then the below not nid one by one add
using web2020apr_p08_t5.Models;
using System.Data;

namespace web2020apr_p08_t5.DAL
{
    public class FlightScheduleDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FlightScheduleDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "FlightConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public List<FlightSchedule> GetAllFlightSchedule()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM FlightSchedule ORDER BY ScheduleID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a FlightSchedule list
            List<FlightSchedule> FlightScheduleList = new List<FlightSchedule>();
            while (reader.Read())
            {
                FlightScheduleList.Add(
                new FlightSchedule
                {
                    ScheduleID = reader.GetInt32(0), //0: 1st column
                    FlightNumber = reader.GetString(1), //1: 2nd column
                    RouteID = reader.GetInt32(2),
                    AircraftID = !reader.IsDBNull(3) ?
                     reader.GetInt32(3) : (int?)null,
                    DepartureDateTime = reader.GetDateTime(4),
                    ArrivalDateTime = reader.GetDateTime(5),
                    EconomyClassPrice = reader.GetDecimal(6),
                    BusinessClassPrice = reader.GetDecimal(7),
                    Status = reader.GetString(8),
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return FlightScheduleList;
        }
        public int Add(FlightSchedule Schedule)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO FlightSchedule (FlightNumber,RouteID, AircraftID, DepartureDateTime, ArrivalDateTime,
                                                    EconomyClassPrice, BusinessClassPrice, Status)
                                                    OUTPUT INSERTED.ScheduleID
                                                    VALUES(@flightnumber,@routeID, @aircraftID, @departuredatetime, @arrivaldatetime,
                                                    @economyclassprice, @businessclassprice, @status)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.

            cmd.Parameters.AddWithValue("@flightnumber", Schedule.FlightNumber);
            cmd.Parameters.AddWithValue("@routeID", Schedule.RouteID);
            cmd.Parameters.AddWithValue("@aircraftID", Schedule.AircraftID);
            cmd.Parameters.AddWithValue("@departuredatetime", Schedule.DepartureDateTime);
            cmd.Parameters.AddWithValue("@arrivaldatetime", Schedule.ArrivalDateTime);
            cmd.Parameters.AddWithValue("@economyclassprice", Schedule.EconomyClassPrice);
            cmd.Parameters.AddWithValue("@businessclassprice", Schedule.BusinessClassPrice);
            cmd.Parameters.AddWithValue("@status", Schedule.Status);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //ScheduleID after executing the INSERT SQL statement
            Schedule.ScheduleID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return Schedule.ScheduleID;
        }

    }
}
