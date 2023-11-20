/**
*--------------------------------------------------------------------
* File name: DataProcessing.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: XXXXXX
* Course-Section: CSCI-2210-XXX
* Creation Date: 11/13/23
* -------------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    public class DataProcessing
    {
        /// <summary>
        /// Takes in information from warehouse object in the state it was in when a crate was unloaded, and writes this information to a file as a comma delimited line.
        /// </summary>
        /// <param name="increment">Time increment of unloading instance.</param>
        /// <param name="crate">Crate being unloaded.</param>
        /// <param name="driver">Driver of crate's truck.</param>
        /// <param name="company">Company of crate's truck.</param>
        /// <param name="trailer">Number of crates left in truck's trailer.</param>
        /// <param name="truckLine">Number of trucks in dock's truck line.</param>
        /// <param name="writer">Writer that is attached to a file. The file is designed to stay open until simulation ends so every unloaded crate gets tracked.</param>
        static public void CrateProcessing(int increment, Crate crate, string driver, string company, int trailer, int truckLine, StreamWriter writer)
        {
            string cratesAndTrucks;
            string toPrint;

            if (trailer > 0)
            {
                cratesAndTrucks = "There are more crates to be unloaded";
            }
            else if (trailer <= 0 && truckLine > 0)
            {
                cratesAndTrucks = "The truck has been unloaded and another truck has taken its place";
            }
            else
            {
                cratesAndTrucks = "The truck has been unloaded but there are no trucks in the line";
            }

            toPrint = $"{increment},{driver},{company},{crate.Id},{crate.Price},{cratesAndTrucks}";

            writer.WriteLine(toPrint);
        }

        /// <summary>
        /// Collects information from warehouse object after it has run a simulation, organizes this info, and writes a report to a file.
        /// </summary>
        /// <param name="warehouse">Warehouse object.</param>
        /// <param name="filePath">Name of file that will be written to. File is saved within bin folder of the Warehouse Simulation project folder.</param>
        static public void SimulationReport(Warehouse warehouse, string filePath)
        {
            int totalCost;
            string toPrint = string.Empty;

            using (StreamWriter writer = new(filePath))
            {
                toPrint += $"Number of docks: {warehouse.Docks.Count}";
                toPrint += $"\nLongest line: {warehouse.LongestLine}";
                toPrint += $"\nTotal trucks: {warehouse.TotalTrucks}";
                toPrint += $"\nTotal crates: {warehouse.TotalCrates}";
                toPrint += $"\nTotal value: {warehouse.TotalValue}";
                toPrint += $"\nAverage value: {(warehouse.TotalValue / warehouse.TotalCrates)}";
                toPrint += $"\nAverage truck value: {(warehouse.TotalTruckValue / warehouse.TotalTrucks)}";
                toPrint += $"\nTime not in use: {((48 * warehouse.Docks.Count) / warehouse.TimeInUse)}";
                toPrint += $"\nAverage time in use: {(warehouse.TimeInUse / warehouse.Docks.Count)}";
                totalCost = warehouse.TimeInUse * 100;
                toPrint += $"\nTotal cost: {totalCost}";
                toPrint += $"\nTotal revenue: {(warehouse.TotalValue - totalCost)}";

                writer.WriteLine(toPrint);
            }
        }
    }
}
