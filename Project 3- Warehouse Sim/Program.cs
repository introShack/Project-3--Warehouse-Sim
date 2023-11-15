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
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "reportTest.txt";
            Warehouse ware = new Warehouse(10);
            ware.FilePath = "test.txt";
            ware.Run();


            DataProcessing.SimulationReport(ware, filePath);



        }

    }
}