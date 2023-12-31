﻿/**
*--------------------------------------------------------------------
* File name: Dock.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: Verdun@etsu.edu
* Course-Section: CSCI-2210-001
* Creation Date: 11/5/23
* -------------------------------------------------------------------
*/




    // __________________________________________________________
    // Oh great Vessel of Honour,
    //  May your servo-motors be guarded,
    //  Against malfunction,
    //   As your spirit is guarded from impurity.
    //     We beseech the Machine God to watch over you.
    //      Let flow the sacred oils,
    //       And let not the sorrows of the Seven Perplexities
    //       trouble thine pistons.
    //        Let flow the blessed unguents,
    //        And may thine circuitry remain divinely blessed.
    //___________________________________________________________
using Project_3__Warehouse_Sim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project_3__Warehouse_Sim

//__________________________________________________________
// Oh great Vessel of Honour,
//  May your servo-motors be guarded,
//   Against malfunction,
//    As your spirit is guarded from impurity.
//     We beseech the Machine God to watch over you.
//      Let flow the sacred oils,
//       And let not the sorrows of the Seven Perplexities
//        trouble thine pistons.
//         Let flow the blessed unguents,
//          And may thine circuitry remain divinely blessed.
//___________________________________________________________
    
{
    /// <summary>
    /// This class will be responsible for much of the simualation data as well as keeping track of the trucks in line 
    /// </summary>
    public class Dock
    {

        public string Id { get; private set; }
        public Queue<Truck> TruckLine;
        public double TotalSales { get; private set; }
        public int TotalCrates { get; set; }
        public int TotalTrucks { get; private set; }
        public int TimeInUse { get; set; }
        public int TimeNotInUse { get; private set; }


        public Dock()
        {
            Id = "NOT ASSIGNED";
            TruckLine = null;
            TotalSales = 0;
            TotalCrates = 0;
            TotalTrucks = 0;
            TimeInUse = 0;
            TimeNotInUse = 0;
        }


        public Dock(string id)
        {
            this.Id = id;
            TruckLine = new Queue<Truck>();
            TotalSales = 0;
            TotalCrates = 0;
            TotalTrucks = 0;
            TimeInUse = 0;
            TimeNotInUse = 0;
        }

        /// <summary>
        /// Adds truck of choice onto queue 
        /// </summary>
        /// <param name="truck"></param>
        public void JoinLine(Truck truck)
        {
            TruckLine.Enqueue(truck);
        }




        /// <summary>
        /// Takes a truck off of queue and returns it
        /// </summary>
        /// <returns> Truck off of line LIFO-Style </returns>
        public Truck SendOff()
        {
            if (TruckLine.Count > 0)
            {
                TotalTrucks++; // to keep track of truck count as when a truck is sent off it is processed, thus counted towards totalTrucks

                return TruckLine.Dequeue();
            }
            else
            {
                throw new IndexOutOfRangeException();
            }

        }

        /// <summary>
        /// Currently just a display of info, used for debugging currently 
        /// </summary>
        /// <returns>A set of data.</returns>
        public override string ToString()
        {

            string dockAsString = string.Empty;

            dockAsString += "Dock ID:" + Id + " \n";
            dockAsString += "Number of Trucks In line: " + TruckLine.Count + "\n";
            dockAsString += "Total amount of sales: " + TotalSales + " \n";
            dockAsString += "Total amount of crates processed: " + TotalCrates + "\n";
            dockAsString += "Total amount of Trucks processed: " + TotalTrucks + "\n";
            dockAsString += "Time dock " + Id + " in use: " + TimeInUse + "\n";
            dockAsString += "Time dock " + Id + " in not use: " + TimeNotInUse + "\n";


            return dockAsString;
        }
    }
}