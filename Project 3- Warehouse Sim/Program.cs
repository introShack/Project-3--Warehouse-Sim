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
            string reportFilePath;
            Warehouse warehouse;

            for (int i = 3; i <= 15; i++)
            {
                warehouse = new Warehouse(i);
                reportFilePath = $"reportTestNum{i}.txt";
                warehouse.FilePath = $"cratesTestNum{i}.txt";
                warehouse.Run();
                DataProcessing.SimulationReport(warehouse, reportFilePath);
            }
        }

    }
}