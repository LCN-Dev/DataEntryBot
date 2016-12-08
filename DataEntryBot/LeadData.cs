using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OpenQA.Selenium.Chrome;


namespace ConsoleApplication1
{
    class LeadData
    {
        // the class we will use to store the data as objects so it will be a queue of data       
        static MySqlConnection sqlConnection;
        static MySqlDataReader reader;
        static MySqlCommand sqlCommand;
        public List<Lead> Queue = new List<Lead>();
        public bool getData()
        {
            string command = @"SELECT * FROM `DATA_ENTRY`";
            sqlConnection = new MySqlConnection();
            sqlCommand = new MySqlCommand(command, sqlConnection);
            sqlConnection.ConnectionString =
           "Server=sql9.freemysqlhosting.net;" +
           "Database=sql9136099;" +
           "Uid=sql9136099;" +
           "Pwd=HvsN6cVwbx;";
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                Lead l = new Lead()
                {          
                    AGENT_NAME = reader.GetString("AGENT_NAME"),
                    LEAD_ID = reader.GetString("LEAD_ID"),
                    LEAD_GUID =  reader.GetString("LEAD_GUID"),
                    IMPORT_ID = reader.GetString("IMPORT_ID"),
                    AGENT_ID = reader.GetString("AGENT_ID"),
                    FIRST_NAME = reader.GetString("FIRST_NAME"),
                    LAST_NAME = reader.GetString("LAST_NAME"),
                    PHONE_NUM = reader.GetString("PHONE_NUM"),
                    PHONE_TYPE = reader.GetString("PHONE_TYPE"),
                    EMAIL = reader.GetString("EMAIL"),
                    ADDRESS = reader.GetString("ADDRESS"),
                    CITY = reader.GetString("CITY"),
                    STATE = reader.GetString("STATE"),
                    ZIP = reader.GetString("ZIP"),
                    RESIDENCE = reader.GetString("RESIDENCE"),
                    RESIDENCE_TYPE = reader.GetString("RESIDENCE_TYPE"),
                    CREDIT = reader.GetString("CREDIT"),
                    INS_PROVIDER = reader.GetString("INS_PROVIDER"),
                    INS_EXP_MONTH = reader.GetString("INS_EXP_MONTH"),
                    INS_EXP_YEAR = reader.GetString("INS_EXP_YEAR"),
                    INS_START_MONTH = reader.GetString("INS_START_MONTH"),
                    INS_START_YEAR = reader.GetString("INS_START_YEAR"),
                    VEHICLE_1_YEAR = reader.GetString("VEHICLE_1_YEAR"),
                    VEHICLE_1_MAKE = reader.GetString("VEHICLE_1_MAKE"),
                    VEHICLE_1_MODEL = reader.GetString("VEHICLE_1_MODEL"),
                    VEHICLE_2_YEAR = reader.GetString("VEHICLE_2_YEAR"),
                    VEHICLE_2_MAKE = reader.GetString("VEHICLE_2_MAKE"),
                    VEHICLE_2_MODEL = reader.GetString("VEHICLE_2_MODEL"),
                    VEHICLE_3_YEAR = reader.GetString("VEHICLE_3_YEAR"),
                    VEHICLE_3_MAKE = reader.GetString("VEHICLE_3_MAKE"),
                    VEHICLE_3_MODEL = reader.GetString("VEHICLE_3_MODEL"),
                    VEHICLE_4_YEAR = reader.GetString("VEHICLE_4_YEAR"),
                    VEHICLE_4_MAKE = reader.GetString("VEHICLE_4_MAKE"),
                    VEHICLE_4_MODEL = reader.GetString("VEHICLE_4_MODEL"),
                    DOB = reader.GetString("DOB"),
                    GENDER = reader.GetString("GENDER"),
                    MARITAL_STATUS = reader.GetString("MARITAL_STATUS"),
                    SPOUSE_FIRST = reader.GetString("SPOUSE_FIRST"),
                    SPOUSE_LAST = reader.GetString("SPOUSE_LAST"),
                    SPOUSE_DOB = reader.GetString("SPOUSE_DOB"),
                    SPOUSE_GENDER = reader.GetString("SPOUSE_GENDER"),                   
                };
                Queue.Add(l);
                l.printLead();                   
            }
            if (Queue.Count > 0)
            {
                sqlConnection.Close();
                return (true);
            }
            else
            {
                sqlConnection.Close();
                return (false);
            }
        }
    }
}
