/**
*--------------------------------------------------------------------
* File name: Program.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: XXXXXX
* Course-Section: CSCI-2210-XXX
* Creation Date: 10/31/23
* -------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    public class Program
    {
        static void Main(string[] args)
        {

            

            string filePath = "reportTest.txt";
            Warehouse ware = new Warehouse(10);
            
            ware.FilePath = "test.txt";
            if(args.Length > 0)
            {
                if (args[0] == "1")
                {
                    ware.Run();
                }
                else if (args[0] == "2")
                {
                    ware.VisualSimTest();
                }
            }
            

            

            //DataProcessing.SimulationReport(ware, filePath);



        }

    }
}