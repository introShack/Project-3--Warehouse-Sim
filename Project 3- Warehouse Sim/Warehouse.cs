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

        private List<Dock> Docks;   //To-Do: Set the number of Docs we will be working with. (Maybe start at 10?)

        private Queue<Truck> Entrance;  // Proposed rule. Anywhere between 1-3 trucks arrive at the Entrance per time increment
                                        // Takes 1 time increment to get a trunk out of the Entrance onto a Dock

        public void Run()   //The simulation that will be run in the driver
        {
            double chanceOfArrival;
            Dock mostEmptyDock = new Dock();

            /* for(int i=0; i<=48; i++)
             * {
             * Crate crateBeingHandled;                                                 //Creates a crate that represents the current crate being handled, to allow for storage of info
             * 
             *      for(int j=0; j<=3; j++)                                             //Loops through three times to determine if a truck arrives (between 1 and 3 per time increment)
             *      {
             *          chanceOfArrival = (rand.Next() % 100) / 100.00;                 //Generates a chance of arrival from 1 to 100, then divides that by 100 to get a percentage (ex. 0.38 = 38%) 
             *          
             *          if(i < 24 && chanceOfArrival <= i/24)                           //If the chance generated is less than the chance designated for the time frame (as per Gillenwater's suggestion)
             *              Entrance.Enqueue(new Truck())                               //A new truck is enqueued into the entrance
             *          else if(i >= 24 && chance <= (48 - i)/ 24)         
             *              Entrance.Enqueue(new Truck())
             *      }
             *      
             *      
             *      if(Entrance is not Null)                                            //If the entrance has a truck in it (meaning it is not Null)
             *      {
             *          foreach(dock in Docks)                                          //The docks are searched
             *          {
             *              if(dock.GetTruckCount() < mostEmptyDock.GetTruckCount())
             *                  mostEmtpyDock = dock;                                   //And the least-filled dock is stored  
             *          }
             *          
             *          mostEmptyDock.JoinLine(Entrance.Dequeue())                      //Once the most emtpy dock has been found, a truck is put into the doc's line.
             *      }
             *      
             *      foreach(Dock in Docks)                                              //Each dock is then allowed to do it's job in this time increment
             *      {
             *          if(Dock.GetLine().Peek().GetTrailer().Peek() == null)           //This peeks into the dock's line (getting a Truck), then peeks into that truck's trailer to get a crate).
             *          {                                                               //If the trailer has no more crates to unload, it will return "null"
             *              Dock.SendOff();                                             //And the truck will be sent off on this time increment
             *          }
             *          else                                                            //If the dock has not needed to send off a Truck
             *          {
             *              crateBeingHandled = Dock.GetLine().Peek().Unload()          //A crate is unloaded and stored in "crateBeingHandled"
             *              
             *              //Store crate information here                              //The information of the crate is all stored and handled here.
             *              //(unsure how this could work. Maybe Aurora's task?)
             *          }
             *      }                                                                   //The loop ends, the time increments, and the process is reapeated
             *  }       
            */
        }

    }
}
