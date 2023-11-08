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
            Driver = MakeName();
            DeliveryCompany = MakeCompany();

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
        public static string MakeName()
        {
            string result = string.Empty;

            //i have 25 diff options for first name and 25 for last name
            //i want a random number generator to pick the number for both and then match that up to the index value in the list of names
            //I am going to start it as just re-reading the list every time it makes a new truck which can create duplicate names since there's only 50 options total... 
            //but we'll see if I can or want to fix it later. probably not very important

            Random rand = new();
            List<string> firstNames = new();
            List<string> lastNames = new();
            string firstNamePath = @"first_names.txt";
            string lastNamePath = @"last_names.txt";
            string firstName,
                lastName;

            using (StreamReader rdr = new StreamReader(firstNamePath))
            {
                while (rdr.Peek() != -1)
                {
                    firstNames.Add(rdr.ReadLine());
                }
            }

            using (StreamReader rdr = new StreamReader(lastNamePath))
            {
                while (rdr.Peek() != -1)
                {
                    lastNames.Add(rdr.ReadLine());
                }
            }

            firstName = firstNames[rand.Next(25)];    //this isn't really the best way to get an accurate random number--optimize later
            lastName = lastNames[rand.Next(25)];

            result += firstName.Trim();
            result += ", ";
            result += lastName.Trim();

            return result;
        }

        public static string MakeCompany()
        {
            string result = string.Empty;
            return result;
        }
    }
}
