using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    internal class DataProcessing
    {
        static public void CrateProcessing()
        { 
            
        }

        static public void SimulationReport(Warehouse warehouse, string filePath)
        {
            int totalCost;
            string toPrint = string.Empty;
            using (StreamWriter writer = new(filePath))
            {
                //is this stupid? should I just do a /n newline implementation instead?

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
