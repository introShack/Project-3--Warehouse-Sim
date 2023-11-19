/**
*--------------------------------------------------------------------
* File name: Warehouse.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: XXXXXX
* Course-Section: CSCI-2210-XXX
* Creation Date: 11/5/23
* -------------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    internal class Warehouse
    {
        public Random rand { get; private set; } //To generate random numbers for truck arrivals

        public List<Dock> Docks { get; private set; }   //To-Do: Set the number of Docs we will be working with. (Maybe start at 10?)
        public Queue<Truck> Entrance { get; private set; }  // Proposed rule. Anywhere between 1-3 trucks arrive at the Entrance per time increment
                                                             // Takes 1 time increment to get a trunk out of the Entrance onto a Dock
        public int TotalTrucks { get; private set; }
        public int TotalCrates { get; private set; }
        public int TimeInUse { get; private set; }
        public double TotalValue { get; private set; }
        public double TotalTruckValue { get; private set; }
        public int LongestLine { get; private set; }

        public string FilePath { get; set; }

        public int CrateNumber { get; private set; }


        public Warehouse(int numOfDocks) 
        {
            rand = new Random();
            Docks = new List<Dock>(numOfDocks);
            Entrance = new Queue<Truck>();
            FilePath = "defaultFilePath.txt";
            CrateNumber = 0;

            for (int i = 1;  i <= numOfDocks; i++)
            {
                Docks.Add(new Dock(i.ToString()));
            }

        }

        public void Run()   //The simulation that will be run in the driver
        {
            StreamWriter writer = new(FilePath);

            try
            {
                Crate crateBeingHandled;            //Creates a crate that represents the current crate being handled, to allow for storage of info


                for (int i = 1; i <= 48; i++)
                {
                    Console.WriteLine($"\n\nNEW TIME INTERVAL {i}");

                    Console.WriteLine("\nTruck arrival time:");

                    TruckArrival(i);

                    Console.WriteLine("\nTruck to dock transferral:");

                    EntranceTransferral();

                    Console.WriteLine("\nTruck unloading:");
                    crateBeingHandled = UnloadTrucks(Docks, i, writer); //The loop ends, the time increments, and the process is repeated
                }

                //save data from docks to variables to be used in simulation report
                foreach (Dock dock in Docks)
                {
                    TotalTrucks += dock.TotalTrucks;
                    TotalCrates += dock.TotalCrates;
                    TimeInUse += dock.TimeInUse;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File could not be found-- did you download everything from the Github?");
            }
            catch (Exception)
            {
                Console.WriteLine("Something strange happened. Try running the simulation again.");
            }
            //ensures that crate report file will be closed once the simulation is done being run
            finally
            {
                writer.Close();
            }
        }

        private void TruckArrival(int i)
        {
            double chanceOfArrival;         //A chance of trucks arriving.
            double chanceOfNoCrate;         //A chance of no more crates being loaded into trucks

            for (int j = 0; j < 3; j++)           //Loops through three times to determine if a truck arrives (between 0 and 3 arrivals per time increment)
            {
                Truck tempTruck = new Truck();          //A temporary truck created to be sent to docks when they are spawned in  

                chanceOfArrival = (rand.Next() % 100) / 100.00;         //Generates a chance of arrival from 1 to 100, then divides that by 100 to get a percentage (ex. 0.38 = 38%) 

                if (i < 24 && chanceOfArrival <= i / 24.00)         //If the chance generated is less than the chance designated for the time frame (as per Gillenwater's suggestion) a truck is spawned at the entrance
                {
                                                //A crate number that increments with every crate loaded into the truck
                    do
                    {
                        chanceOfNoCrate = (rand.Next() % 100) / 100.00;         //A randomized chanace for the crates to stop being loaded into the truck

                        tempTruck.Load(new Crate(CrateNumber.ToString()));      //Loads the crate into the truck and increments the crate number for Id purposes
                        CrateNumber++;
                    } while (tempTruck.Trailer.Count < 12 && chanceOfNoCrate < 0.80);           //80% chance of the crates no longer being loaded into the truck per loop.

                    Entrance.Enqueue(tempTruck);                               //A new truck is enqueued into the entrance

                    Console.WriteLine($"\tTruck from {tempTruck.DeliveryCompany} was put in the entrance");
                }
                else if (i >= 24 && chanceOfArrival <= (48 - i) / 24.00)            //The same code as above, but with a different chance of arrival, as per Gillenwater's suggestion)
                {
                    do
                    {
                        chanceOfNoCrate = (rand.Next() % 100) / 100.00;

                        tempTruck.Load(new Crate(CrateNumber.ToString()));
                        CrateNumber++;                                              //increase crate number every time a crate is added to a truck for the simulation report
                    } while (tempTruck.Trailer.Count < 12 && chanceOfNoCrate < 0.80);

                    Entrance.Enqueue(tempTruck);
                    Console.WriteLine($"\tTruck from {tempTruck.DeliveryCompany} was put in the entrance");
                }
            }
        }

        private void EntranceTransferral()
        {
            Dock mostEmptyDock = new Dock("-1");            //The most empty dock in the docks of the warehouse, initialized into a unused dock for the purpose of comparison

            for (int j = 0; j < 2; j++)         //Loop that dequeues at most two trucks into the least filled dock
            {
                if (Entrance.Count != 0)                                            //If the entrance has a truck in it 
                {

                    Console.WriteLine($"\tEntrance has {Entrance.Count} trucks in it!");

                    foreach (Dock dock in Docks)                                     //The docks are searched
                    {
                        if (dock.TruckLine.Count() < mostEmptyDock.TruckLine.Count() || mostEmptyDock.Id == "-1")
                            mostEmptyDock = dock;                                   //And the least-filled dock is stored

                        //keep track of the longest dock line for simulation report
                        if (dock.TruckLine.Count > LongestLine)
                        {
                            LongestLine = dock.TruckLine.Count;
                        }
                    }

                    Console.WriteLine($"\tTruck from {Entrance.Peek().DeliveryCompany} was sent to dock {mostEmptyDock.Id} it has {Entrance.Peek().Trailer.Count} crates in it.");

                    for (int k = 0; k < Entrance.Peek().Trailer.Count; k++)
                    {
                        TotalTruckValue += Entrance.Peek().Trailer.Peek().Price;
                    }

                    mostEmptyDock.JoinLine(Entrance.Dequeue());                      //Once the most empty dock has been found, a truck is put into the dock's line
                }
            }

        }

        private Crate UnloadTrucks(List<Dock> docks, int increment, StreamWriter writer)
        {
            Crate crate = new Crate();
            foreach (Dock dock in docks)                                         //Each dock is then allowed to do it's job in this time increment
            {

                Truck truckBeingWorkedOn = new Truck();           //A temporary truck is made to be initialized to the truck being worked on in each dock

                if (dock.TruckLine.Count != 0)
                {
                    truckBeingWorkedOn = dock.TruckLine.Peek();       //If there is a truck in the dock, the top truck is initialized to the temporary truck to be worked on
                }

                if (dock.TruckLine.Count == 0)           //If there are no trucks in line, the loop simply continues
                {
                    continue;
                }
                else if (truckBeingWorkedOn.Trailer.Count != 0)           //If a truck's trailer has crates in it (meaning it's non-zero), then a crate is unloaded
                {
                    crate = truckBeingWorkedOn.Unload();          //A crate is unloaded and stored in "crate"

                    //every time a crate is unloaded, crateProcessing takes in necessary information and copies into crate report file
                    DataProcessing.CrateProcessing(increment, crate, truckBeingWorkedOn.Driver, truckBeingWorkedOn.DeliveryCompany, truckBeingWorkedOn.Trailer.Count, dock.TruckLine.Count, writer);

                    //update dock variables whenever a crate is unloaded to use for simulation report
                    TotalValue += crate.Price;
                    dock.TotalCrates++;
                    dock.TimeInUse++;

                    Console.WriteLine($"\t{truckBeingWorkedOn.DeliveryCompany} on dock {dock.Id} was unloaded a crate. The truck now has {truckBeingWorkedOn.Trailer.Count} crates left to unload.");

                }

                if (truckBeingWorkedOn.Trailer.Count == 0)          //If the trailer is now empty in the truck, the truck is sent off for a new one to take its place
                {
                    dock.SendOff();
                    Console.WriteLine($"\t\tTruck from {truckBeingWorkedOn.DeliveryCompany} finished unloading and was sent off of dock {dock.Id}");
                }

            }

            return crate;
        }
    }
}
