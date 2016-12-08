using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using MySql.Data.MySqlClient;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace ConsoleApplication1
{
    class Lead        
    {
        static ChromeDriverService serve = ChromeDriverService.CreateDefaultService();
        static ChromeDriver Cdriver = new ChromeDriver();

        public string AGENT_NAME { get; set; } = "";
        public string LEAD_ID { get; set; } = "";
        public string LEAD_GUID { get; set; } = "";
        public string IMPORT_ID { get; set; } = "";
        public string AGENT_ID { get; set; } = "";
        public string FIRST_NAME { get; set; } = "";
        public string LAST_NAME { get; set; } = "";
        public string PHONE_NUM { get; set; } = "";
        public string PHONE_TYPE { get; set; } = "";
        public string EMAIL { get; set; } = "";
        public string ADDRESS { get; set; } = "";
        public string CITY { get; set; } = "";
        public string STATE { get; set; } = "";
        public string ZIP { get; set; } = "";
        public string RESIDENCE { get; set; } = "";
        public string RESIDENCE_TYPE { get; set; } = "";
        public string CREDIT { get; set; } = "";
        public string INS_PROVIDER { get; set; } = "";
        public string INS_EXP_MONTH { get; set; } = "";
        public string INS_EXP_YEAR { get; set; } = "";
        public string INS_START_MONTH { get; set; } = "";
        public string INS_START_YEAR { get; set; } = "";
        public string VEHICLE_1_YEAR { get; set; } = "";
        public string VEHICLE_1_MAKE { get; set; } = "";
        public string VEHICLE_1_MODEL { get; set; } = "";
        public string VEHICLE_2_YEAR { get; set; } = "";
        public  string VEHICLE_2_MAKE { get; set; } = "";
        public string VEHICLE_2_MODEL { get; set; } = "";
        public string VEHICLE_3_YEAR { get; set; } = "";
        public string VEHICLE_3_MAKE { get; set; } = "";
        public string VEHICLE_3_MODEL { get; set; } = "";
        public string VEHICLE_4_YEAR { get; set; } = "";
        public string VEHICLE_4_MAKE { get; set; } = "";
        public string VEHICLE_4_MODEL { get; set; } = "";
        public string DOB { get; set; } = "";
        public string GENDER { get; set; } = "";
        public string MARITAL_STATUS { get; set; } = "";
        public string SPOUSE_FIRST { get; set; } = "";
        public string SPOUSE_LAST { get; set; } = "";
        public string SPOUSE_DOB { get; set; } = "";
        public string SPOUSE_GENDER { get; set; } = "";
        public Lead()
        {
            serve.HideCommandPromptWindow = true;
        }
        public void printLead()
        {
            Console.WriteLine("First Name: " + FIRST_NAME);
            Console.WriteLine("Last  Name: " + LAST_NAME);
        }

        public void removeLead()
        {
            string command = @"DELETE FROM `DATA_ENTRY` WHERE `AGENT_NAME` = " + '"' + AGENT_NAME + '"' + " AND  `LEAD_ID` = " + '"' + LEAD_ID + '"' + " AND  `LEAD_GUID` = " + '"' + LEAD_GUID + '"';
            MySqlConnection sqlConnection = new MySqlConnection();
            MySqlCommand sqlCommand = new MySqlCommand(command, sqlConnection);
            sqlConnection.ConnectionString =
           "Server=sql9.freemysqlhosting.net;" +
           "Database=sql9136099;" +
           "Uid=sql9136099;" +
           "Pwd=HvsN6cVwbx;";
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();

        }
        public static bool unhideElement(string elementId)
        {
            try
            {
                Cdriver.ExecuteScript("$('" + elementId + "').removeClass('hide')");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool EnterSelectElementData(string elementId, string data)
        {
            bool retry = true;
            int retrycount = 0;
            int staleRefCount = 0;
            int unhideCount = 0;

            while (retry)
            {
                try
                {
                    var select = new  SelectElement(Cdriver.FindElementById(elementId));

                    select.SelectByText(data);
                    return true;
                }
                catch (OpenQA.Selenium.ElementNotVisibleException)
                {
                    unhideElement(elementId);
                    Console.WriteLine("Element has been unhidden, retrying...");
                    if (unhideCount == 1)
                    {
                        Console.WriteLine("couldn't unhide, ending...");
                        retry = false;
                        return false;
                    }
                    unhideCount += 1;
                }
                catch (OpenQA.Selenium.NoSuchElementException ex)
                {
                    string message = ex.Message;
                    if (message.Contains(data))
                    {
                        Console.WriteLine(data + " is not a valid option for " + elementId + "; try a different option.");
                        if (retrycount < 2)
                        {
                             Thread.Sleep(500);
                        }
                        else
                        {
                            retry = false;
                            return false;
                        }
                        retrycount += 1;
                    }
                    else
                    {
                        Console.WriteLine(elementId + " does not exist on the current form. Try a different ID?");
                    }

                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    if (staleRefCount == 2)
                    {
                        Console.WriteLine("Two stale references, ending");
                        retry = false;
                    }
                    Thread.Sleep(1000);
                    staleRefCount += 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generic Exception");
                    Console.WriteLine("Inner exception: " + ex.InnerException);
                    Console.WriteLine("Message: " + ex.Message);
                    retry = false;
                }
            }
            return false;
        }
        public static bool EnterData(string elementId, string data)
        {
            bool retry = true;
            int staleRefCount = 0;
            int unhideCount = 0;

            while (retry)
            {
                try
                {
                    Cdriver.FindElementById(elementId).Clear();
                    Cdriver.FindElementById(elementId).SendKeys(data);
                    return true;
                }
                catch (OpenQA.Selenium.ElementNotVisibleException)
                {
                    unhideElement(elementId);
                    Console.WriteLine("Element has been unhidden, retrying...");
                    if (unhideCount == 1)
                    {
                        Console.WriteLine("couldn't unhide, ending...");
                        retry = false;
                        return false;
                    }
                    unhideCount += 1;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    Console.WriteLine(elementId + " does not exist on the current form. Try a different ID?");
                    retry = false;
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    if (staleRefCount == 2)
                    {
                        Console.WriteLine("Two stale references, ending");
                        retry = false;
                    }
                    Thread.Sleep(1000);
                    staleRefCount += 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generic Exception");
                    Console.WriteLine("Inner exception: " + ex.InnerException);
                    Console.WriteLine("Message: " + ex.Message);
                    retry = false;
                }
            }
            return false;
        }

        public bool EnterLead()
        {                   
            try
            {
                Cdriver.Navigate().GoToUrl("https://forms.lead.co/auto/?agent_name=" + AGENT_NAME + "&lead_id=" + LEAD_ID + "&lead_guid=" + LEAD_GUID + "&import_id=" + IMPORT_ID + "&agent_id=" + AGENT_ID);
                EnterData(LeadFormElements.FIRST_NAME, FIRST_NAME);
                EnterData(LeadFormElements.LAST_NAME, LAST_NAME);
                EnterSelectElementData(LeadFormElements.PHONE_TYPE, PHONE_TYPE);
                EnterData(LeadFormElements.ADDRESS, ADDRESS);
                EnterData(LeadFormElements.CITY, CITY);
                EnterSelectElementData(LeadFormElements.STATE, STATE);
                EnterData(LeadFormElements.ZIP, ZIP);
                EnterData(LeadFormElements.OWN_RENT, RESIDENCE);
                EnterData(LeadFormElements.RESIDENCE_TYPE, RESIDENCE_TYPE);
                EnterData(LeadFormElements.ZIP, ZIP);

                EnterSelectElementData(LeadFormElements.CREDIT_RATING, CREDIT);
                EnterSelectElementData(LeadFormElements.INSURANCE_PROVIDER, INS_PROVIDER);
                EnterSelectElementData(LeadFormElements.INSURANCE_EXPIRATION_MONTH, INS_EXP_MONTH);

                EnterData(LeadFormElements.INSURANCE_EXPIRATION_YEAR, INS_EXP_YEAR);
                EnterSelectElementData(LeadFormElements.INSURANCE_START_MONTH, INS_START_YEAR);

                EnterSelectElementData(LeadFormElements.VEHICLE_1_YEAR, VEHICLE_1_YEAR);
                EnterSelectElementData(LeadFormElements.VEHICLE_1_MAKE, VEHICLE_1_MAKE);
                EnterSelectElementData(LeadFormElements.VEHICLE_1_MODEL, VEHICLE_1_MODEL);

                EnterSelectElementData(LeadFormElements.VEHICLE_2_YEAR, VEHICLE_2_YEAR);
                EnterSelectElementData(LeadFormElements.VEHICLE_2_MAKE, VEHICLE_2_MAKE);
                EnterSelectElementData(LeadFormElements.VEHICLE_2_MODEL, VEHICLE_2_MODEL);

                EnterSelectElementData(LeadFormElements.VEHICLE_3_YEAR, VEHICLE_3_YEAR);
                EnterSelectElementData(LeadFormElements.VEHICLE_3_MAKE, VEHICLE_3_MAKE);
                EnterSelectElementData(LeadFormElements.VEHICLE_3_MODEL, VEHICLE_3_MODEL);

                EnterSelectElementData(LeadFormElements.VEHICLE_4_YEAR, VEHICLE_4_YEAR);
                EnterSelectElementData(LeadFormElements.VEHICLE_4_MAKE, VEHICLE_4_MAKE);
                EnterSelectElementData(LeadFormElements.VEHICLE_4_MODEL, VEHICLE_4_MODEL);

                EnterSelectElementData(LeadFormElements.DOB_DAY, VEHICLE_1_YEAR);
                EnterSelectElementData(LeadFormElements.DOB_MONTH, VEHICLE_1_MAKE);
                EnterSelectElementData(LeadFormElements.DOB_YEAR, VEHICLE_1_MODEL);

            }
            catch
            {
                Console.WriteLine("NOT ENOUGH DATA");
            }
            return false;
        }
    }
}
