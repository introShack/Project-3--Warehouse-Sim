using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    public class DataProcessing
    {
        static public void CrateProcessing(int increment, Crate crate, string driver, string company, int trailer, int truckLine, StreamWriter writer)
        {
            string cratesAndTrucks;

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
                cratesAndTrucks = "The truck has been unloaded, but there are no trucks in the line";
            }

            writer.WriteLine($"{increment},{driver},{company},{crate.Id},{crate.Price},{cratesAndTrucks}");
        }

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
