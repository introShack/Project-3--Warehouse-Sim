/**
*--------------------------------------------------------------------
* File name: Truck.cs
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project_3__Warehouse_Sim
{
    internal class Truck
    {
        public string Driver { get; private set; }
        public string DeliveryCompany { get; private set; }

        public Stack<Crate> Trailer { get; private set; }


        public Truck()
        {
            //maybe something with the randomizing of driver and company names? or should that be a paramaterized constructor...?
            //I'm gonna put this in here for now but should talk with the group about whether we want to have a different method or class to
            //randomize the driver and delivery companies. Also, where will the files be saved? is that a github thing?
            Driver = null;
            DeliveryCompany = null;

            Trailer = new Stack<Crate>();   //default empty stack for now-- not sure if we'd ever want to change the initial size
            
        }

        /// <summary>
        /// Adds a crate to the truck's trailer from back to front.
        /// </summary>
        /// <param name="crate">Crate to be added to truck.</param>
        public void Load(Crate crate)
        {
            Trailer.Push(crate);
        }

        /// <summary>
        /// Removes the front-most crate from the truck's trailer.
        /// </summary>
        /// <returns>Crate that was just removed.</returns>
        public Crate Unload()
        {
           //program will need logic for when the truck is empty
            return Trailer.Pop();
        }

        // do these methods belong in the truck class? should they be moved somewhere else? I don't think so...
        public string MakeName()
        {
            string result = string.Empty;
            return result;
        }

        public string MakeCompany()
        {
            string result = string.Empty;
            return result;
        }
    }
}
