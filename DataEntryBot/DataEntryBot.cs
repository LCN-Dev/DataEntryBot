using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
// MAIN ENTRY POINT
namespace ConsoleApplication1
{
    class DataEntryBot
    {
        static LeadData Data = new LeadData();
        static void Main(string[] args)
        {
            Console.WriteLine("Checking for data...");
            while (true)
            {                
                if(Data.getData())              // IF DATA IS FOUND FROM THE SQL QUERY
                {
                    Console.WriteLine("Data Found...Starting Lead Entry Process");             
                    while(Data.Queue.Count > 0)
                    {
                        Data.Queue[0].EnterLead();
                        Data.Queue[0].removeLead();
                        Data.Queue.RemoveAt(0);
                    }     
                }

                else
                {
                    Thread.Sleep(1000);         // ONLY 1 QUERY PER SECOND
                }
            }
        }
    }
}
