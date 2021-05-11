using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using web2020apr_p08_t5.Models;
using System.Data;


namespace web2020apr_p08_t5.DAL
{
    public class StaffDAL 
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        
        public StaffDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string strConn = Configuration.GetConnectionString("FlightConnectionString");
            
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<Staff> GetAllStaff()
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
            List<Staff> staffList = new List<Staff>();
            while (reader.Read())
            {
                staffList.Add(
                    new Staff
                    {
                        StaffId = reader.GetInt32(0),
                        StaffName = reader.GetString(1),
                        Gender = reader.IsDBNull(2)? reader.GetChar(2):(Char?)null,
                        DateEmployed = reader.IsDBNull(3)? reader.GetDateTime(3):(DateTime?)null,
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

            return staffList;

        }

        public string Add(Staff staff)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"insert into Staff (StaffName, StaffId, Gender, EmailAddr, Vocation, Date)
                                                   values(@StaffName, @StaffId, @Gender, @EmailAddr, @Vocation, @Date)";
            
            cmd.Parameters.AddWithValue("@StaffName", staff.StaffName);
            cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
            cmd.Parameters.AddWithValue("@Gender", staff.Gender);
            cmd.Parameters.AddWithValue("@EmailAddr", staff.EmailAddr);
            cmd.Parameters.AddWithValue("@Vocation", staff.Vocation);
            cmd.Parameters.AddWithValue("@Date", staff.DateEmployed);

            conn.Open();
            staff.StaffName = (string)cmd.ExecuteScalar();
            conn.Close();

            return staff.StaffName;
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

    }
}
