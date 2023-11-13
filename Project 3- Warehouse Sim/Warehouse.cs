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
        Random rand = new Random(); //To generate random numbers for truck arrivals

        private List<Dock> Docks = new List<Dock>(10);   //To-Do: Set the number of Docs we will be working with. (Maybe start at 10?)
         
        private Queue<Truck> Entrance = new Queue<Truck>();  // Proposed rule. Anywhere between 1-3 trucks arrive at the Entrance per time increment
                                        // Takes 1 time increment to get a trunk out of the Entrance onto a Dock
        public Warehouse() 
        { 
            for (int i = 1;  i <= 10; i++)
            {
                Docks.Add(new Dock(i.ToString()));
            }
        }

        public void Run()   //The simulation that will be run in the driver
        {
            double chanceOfArrival;
            double chanceOfNoCrate;
            Dock mostEmptyDock = new Dock("-1");
            Crate crateBeingHandled;                                                 //Creates a crate that represents the current crate being handled, to allow for storage of info


            for (int i=1; i<=48; i++)
            {
                Console.WriteLine($"\n\nNEW TIME INTERVAL {i}");

                Console.WriteLine("\nTruck arrival time:");

                for (int j=0; j < 3; j++)                                             //Loops through three times to determine if a truck arrives (between 1 and 3 per time increment)
                {
                    Truck tempTruck = new Truck();

                    chanceOfArrival = (rand.Next() % 100) / 100.00;                 //Generates a chance of arrival from 1 to 100, then divides that by 100 to get a percentage (ex. 0.38 = 38%) 

                    if (i < 24 && chanceOfArrival <= i / 24.00)                           //If the chance generated is less than the chance designated for the time frame (as per Gillenwater's suggestion)
                    {
                        int crateNumber = 0;
                        do
                        {
                            chanceOfNoCrate = (rand.Next() % 100) / 100.00;

                            tempTruck.Load(new Crate(crateNumber.ToString()));
                            crateNumber++;
                        } while (tempTruck.Trailer.Count < 12 && chanceOfNoCrate < 0.90);

                        Entrance.Enqueue(tempTruck);                               //A new truck is enqueued into the entrance
                        Console.WriteLine($"\tTruck from {tempTruck.DeliveryCompany} was put in the entrance");
                        
                    }
                    else if (i >= 24 && chanceOfArrival <= (48 - i) / 24.00)
                    {
                        int crateNumber = 0;
                        do
                        {
                            chanceOfNoCrate = (rand.Next() % 100) / 100.00;

                            tempTruck.Load(new Crate(crateNumber.ToString()));
                            crateNumber++;
                        } while (tempTruck.Trailer.Count < 12 && chanceOfNoCrate < 0.90);

                        Entrance.Enqueue(tempTruck);                               //A new truck is enqueued into the entrance
                        Console.WriteLine($"\tTruck from {tempTruck.DeliveryCompany} was put in the entrance");
                    }
                }

                Console.WriteLine("\nTruck to dock transferral:");

                for (int j = 0; j < 2; j++)
                {
                    if (Entrance.Count != 0)                                            //If the entrance has a truck in it (meaning it is not Null)
                    {

                        Console.WriteLine($"\tEntrance has {Entrance.Count} trucks in it!");

                        foreach (Dock dock in Docks)                                     //The docks are searched
                        {
                            if (dock.TruckLine.Count() < mostEmptyDock.TruckLine.Count() || mostEmptyDock.Id == "-1")
                                mostEmptyDock = dock;                                   //And the least-filled dock is stored  
                        }


                        Console.WriteLine($"\tTruck from {Entrance.Peek().DeliveryCompany} was sent to dock {mostEmptyDock.Id} it has {Entrance.Peek().Trailer.Count} crates in it.");

                        mostEmptyDock.JoinLine(Entrance.Dequeue());                      //Once the most emtpy dock has been found, a truck is put into the doc's line.

                    }
                }
                
                int dockNumber = 0;

                Console.WriteLine("\nTruck unloading:");

                foreach (Dock dock in Docks)                                         //Each dock is then allowed to do it's job in this time increment
                {
                    
                    dockNumber++;
                    Truck temp = new Truck();
                    if(dock.TruckLine.Count != 0)
                    {
                        temp = dock.TruckLine.Peek();
                    }

                    if(dock.TruckLine.Count == 0)
                    {
                        continue;
                    }
                    else if(temp.Trailer.Count == 0)           //This peeks into the dock's line (getting a Truck), then peeks into that truck's trailer to get a crate).
                    {                                                               //If the trailer has no more crates to unload, it will return "null"
                        dock.SendOff();                                             //And the truck will be sent off on this time increment
                        Console.WriteLine($"\tTruck from {temp.DeliveryCompany} finished unloading and was sent off of dock {dock.Id}");
                    }
                    else                                                            //If the dock has not needed to send off a Truck
                    {
                        
                        crateBeingHandled = temp.Unload();          //A crate is unloaded and stored in "crateBeingHandled"
                        Console.WriteLine($"\t{temp.DeliveryCompany} on dock {dock.Id} was unloaded a crate. The truck now has {temp.Trailer.Count} crates left to unload.");
                          
                          //Store crate information here                              //The information of the crate is all stored and handled here.
                          //(unsure how this could work. Maybe Aurora's task?)
                    }
                    
                }                                                                   //The loop ends, the time increments, and the process is reapeated
            }       
          
        }

    }
}
