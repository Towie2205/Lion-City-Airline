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
    public class FlightPersonnelDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public FlightPersonnelDAL()
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

        public List<FlightPersonnel> GetAllFlightPersonnel()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Staff ORDER BY StaffID";
            //Open a database connection
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a staff list
            List<FlightPersonnel> flightpersonnelList = new List<FlightPersonnel>();
            while (reader.Read())
            {
                flightpersonnelList.Add(
                    new FlightPersonnel
                    {
                        StaffId = reader.GetInt32(0),
                        StaffName = reader.GetString(1),
                        //Gender = reader.IsDBNull(2) ? reader.GetChar(2) : (Char?)null,
                        Gender = reader.GetString(2)[0],
                        //DateEmployed = reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null,
                        DateEmployed = reader.GetDateTime(3),
                        Vocation = reader.GetString(4),
                        EmailAddr = reader.GetString(5),
                        Password = reader.GetString(6),
                        Status = reader.GetString(7),
                    }
                    );
            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            return flightpersonnelList;

        }

        public int Add(FlightPersonnel Personnel)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Staff (StaffName, Gender, EmailAddr, Vocation, DateEmployed)
                                                    OUTPUT INSERTED.StaffId
                                                   VALUES(@StaffName, @Gender, @EmailAddr, @Vocation, @DateEmployed)";
            

            cmd.Parameters.AddWithValue("@StaffName", Personnel.StaffName);
            //cmd.Parameters.AddWithValue("@StaffId", flightPersonnel.StaffId);
            cmd.Parameters.AddWithValue("@Gender", Personnel.Gender);
            cmd.Parameters.AddWithValue("@EmailAddr", Personnel.EmailAddr);
            cmd.Parameters.AddWithValue("@Vocation", Personnel.Vocation);
            cmd.Parameters.AddWithValue("@DateEmployed", Personnel.DateEmployed);

            conn.Open();

            Personnel.StaffId = (int)cmd.ExecuteScalar(); //object reference not set to an instance of an object (error) FIXED
            //Personnel.Status = (string)cmd.ExecuteScalar();
            //Personnel.Password = (string)cmd.ExecuteScalar();

            conn.Close();

            return Personnel.StaffId;
            
        }

        public bool DoesStaffExist(int StaffId)
        {
            bool staffFound = false;
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"select StaffId from Staff where userid=@selectedStaffId";
            cmd.Parameters.AddWithValue("@selectedStaffId", StaffId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != StaffId)
                    {
                        staffFound = true;
                    }
                }
            }

            reader.Close();
            conn.Close();

            return staffFound;
        }

        public int Delete(int staffId)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Staff
                                WHERE StaffID = @selectStaffID";
            cmd.Parameters.AddWithValue("@selectStaffID", staffId);

            //open db
            conn.Open();

            int rowAffected = 0;

            //execute the delte sql to remove staff record
            rowAffected += cmd.ExecuteNonQuery();

            //close db connection
            conn.Close();

            //return number of row of staff record updated
            return rowAffected;
        }

        public FlightPersonnel GetDetails(int staffId)
        {
            FlightPersonnel flightpersonnel = new FlightPersonnel();

            //cr8 sql command object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify an INSERT SQL statement which will      
            //return the auto-generated StaffID after insertion      
            cmd.CommandText = @"SELECT * FROM Staff WHERE StaffID = @selectStaffID";

            //Define the parameters used in SQL statement, value for each parameter     
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@selectStaffID", staffId);

            //open db
            conn.Open();
            //execute select sql through datareader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //read record from db
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    flightpersonnel.StaffId = staffId;
                    flightpersonnel.StaffName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    // (char) 0 - ASCII Code 0 - null value
                    flightpersonnel.Gender = !reader.IsDBNull(2) ?
                        reader.GetString(2)[0] : (char)0;
                    flightpersonnel.DateEmployed = !reader.IsDBNull(3) ?
                        reader.GetDateTime(3) : (DateTime?)null;
                    flightpersonnel.Vocation = !reader.IsDBNull(4) ?
                        reader.GetString(4) : null;
                    
                    flightpersonnel.EmailAddr = !reader.IsDBNull(5) ?
                        reader.GetString(5) : null;
                    flightpersonnel.Password = !reader.IsDBNull(6) ?
                        reader.GetString(6) : null;
                    flightpersonnel.Status = !reader.IsDBNull(7) ?
                        reader.GetString(7) : null;
                }
            }
            //close data reader
            reader.Close();
            //close db
            conn.Close();

            return flightpersonnel;
        }
    }
}
