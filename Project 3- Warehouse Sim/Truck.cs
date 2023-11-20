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
    public class Truck
    {
        public string Driver { get; private set; }
        public string DeliveryCompany { get; private set; }

        public Stack<Crate> Trailer { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Truck()
        {
            Driver = MakeName();
            DeliveryCompany = MakeCompany();

            Trailer = new Stack<Crate>(); 

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

        /// <summary>
        /// Randomizes a name for truck object using included text files of 25 first names and 25 last names.
        /// </summary>
        /// <returns>Full name of the truck driver.</returns>
        public static string MakeName()
        {
            string name = string.Empty;

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

            firstName = firstNames[rand.Next(firstNames.Count)];
            lastName = lastNames[rand.Next(lastNames.Count)];

            name += firstName.Trim();
            name += " ";
            name += lastName.Trim();

            return name;
        }

        /// <summary>
        /// Randomizes a company for truck object using included text files of 25 adjectives and 25 nouns.
        /// </summary>
        /// <returns>Full company name for truck.</returns>
        public static string MakeCompany()
        {
            string company = string.Empty;

            Random rand = new();
            List<string> adjectives = new();
            List<string> nouns = new();
            string adjPath = @".\Stuff\adjectives.txt";
            string nounPath = @".\Stuff\nouns.txt";
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
    }
}
