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

        //ALSO STILL NEEDS OPTIMIZING BECAUSE WHY DON'T I JUST MAKE IT A SET METHOD INSTEAD OF ITS OWN STATIC THING
        //or, maybe this method should be moved to the warehouse class whenever it's done?
        public static string MakeName()
        {
            string name = string.Empty;

            //I am going to start it as just re-reading the list every time it makes a new truck which can create duplicate names since there's only 50 options total... 
            //but we'll see if I can or want to fix it later. probably not very important

            Random rand = new();
            List<string> firstNames = new();
            List<string> lastNames = new();
            string firstNamePath = @".\Stuff\first_names.txt";
            string lastNamePath = @".\Stuff\last_names.txt";
            string firstName,
                lastName;

            using (StreamReader rdr = new StreamReader(firstNamePath))
            using (StreamReader rdr2 = new StreamReader(lastNamePath))
            {
                while (rdr.Peek() != -1 && rdr2.Peek() != -1)
                {
                    firstNames.Add(rdr.ReadLine());
                    lastNames.Add(rdr2.ReadLine());
                }
            }

            firstName = firstNames[rand.Next(firstNames.Count)];    //this isn't really the best way to get an accurate random number--optimize later
            lastName = lastNames[rand.Next(lastNames.Count)];

            name += firstName.Trim();
            name += " ";
            name += lastName.Trim();

            return name;
        }

        public static string MakeCompany()
        {
            string company = string.Empty;

            Random rand = new();
            List<string> adjectives = new();
            List<string> nouns = new();
            string adjPath = @"C:\Users\marro\OneDrive - East Tennessee State University\Data structures\Project 3- Warehouse Sim\adjectives.txt";
            string nounPath = @"C:\Users\marro\OneDrive - East Tennessee State University\Data structures\Project 3- Warehouse Sim\nouns.txt";
            string adjective,
                noun;

            using (StreamReader rdr = new StreamReader(adjPath))
            using (StreamReader rdr2 = new StreamReader(nounPath))
            {
                Console.SetIn(rdr);
                while (rdr.Peek() != -1 && rdr2.Peek() != -1)
                {
                    adjectives.Add(rdr.ReadLine());
                    nouns.Add(rdr2.ReadLine());
                }
            }

            adjective = adjectives[rand.Next(adjectives.Count)];
            noun = nouns[rand.Next(nouns.Count)];

            company += adjective.Trim();
            company += noun.Trim();

            return company;
        }


        //method that will totally be in warehouse class:
        //csv crate information
        
        //every time a crate is unloaded in warehouse, it should save to a file. I think the file should actually be initialized in the driver
        //because there's not really another way to not re create a file every time
        //but that begs the question, how is warehouse supposed to access a file that exists in program?

        //there also needs to be extra logic for the unload status that checks if truck is empty after crate unloads, if another truck was in the dock when empty,
        //or if not

        
        


    }
}
