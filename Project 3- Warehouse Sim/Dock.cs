/**
*--------------------------------------------------------------------
* File name: Dock.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: XXXXXX
* Course-Section: CSCI-2210-XXX
* Creation Date: 11/5/23
* -------------------------------------------------------------------
*/

using Project_3__Warehouse_Sim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    internal class Dock
    {

        public string Id { get; private set; }
        Queue<Truck> TruckLine;
        public double TotalSales { get; private set; }
        public int TotalCrates { get; private set; }
        public int TotalTrucks { get; private set; }
        public int TimeInUse { get; private set; }
        public int TimeNotInUse { get; private set; }


        public Dock()
        {
            Id = "NOT ASSIGNED";
            TruckLine = null;
        }


        public Dock(string id)
        {
            this.Id = id;
            TruckLine = new Queue<Truck>();

        }


        public void JoinLine(Truck truck)
        {


            TruckLine.Enqueue(truck);

        }




        /// <summary>
        /// Takes a truck off of queue and returns it, 
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


                return null;  // don't know how to handle this yet, thinking about throwing a exception, till then null will work

            }






        }

        /// <summary>
        /// Used to take crates from trucks randomized values and sum them up to find total amount of crates processed. 
        /// </summary>
        /// <param name="amountOfCratesAdded"></param>
        public void keepTrackOfCrates(int amountOfCratesAdded)
        {
            TotalCrates += amountOfCratesAdded;
        }

        // implement way to change values 
        /// <summary>
        /// TimeKeeper intended to be called everytime the dock is in use, increments in unit of time used by main simulation
        /// Also will use this to find TimeNotInUse via subtraction
        /// </summary>
        public void TimeKeeper()
        {
            TimeInUse++;
        }
        /// <summary>
        /// Currently just a display of info, used for debugging currently 
        /// </summary>
        /// <returns>A set of data(intending on changing this to a more vistual repersentation of the dock)</returns>
        public override string ToString()
        {

            string dockAsString = string.Empty;

            dockAsString += Id + " \n";
            dockAsString += TruckLine.Count + "\n";
            dockAsString += TotalSales + " \n";
            dockAsString += TotalCrates + "\n";
            dockAsString += TotalTrucks + "\n";
            dockAsString += TimeInUse + "\n";
            dockAsString += TimeNotInUse + "\n";


            return dockAsString;


        }


    }
}