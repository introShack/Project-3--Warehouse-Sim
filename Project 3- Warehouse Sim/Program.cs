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
            Warehouse ware = new Warehouse();
            ware.Run();

            Console.WriteLine(ware.TotalValue);
            Console.WriteLine(ware.TotalTruckValue);
            Console.WriteLine(ware.TotalTrucks);
            Console.WriteLine(ware.TotalCrates);
            Console.WriteLine(ware.TimeInUse);
            Console.WriteLine(ware.LongestLine);

            //WAIT A SECOND
            //ARENT TOTAL CRATES AND TIME IN USE THE SAME FUCKIN THING
        }

    }
}