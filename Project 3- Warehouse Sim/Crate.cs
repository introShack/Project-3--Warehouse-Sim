/**
*--------------------------------------------------------------------
* File name: Crate.cs
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
    internal class Crate
    {
        public Random rand = new Random();

        private string Id;

        private double Price;

        public Crate()
        {
            Price = (rand.Next() % 450) + 50 + ((rand.Next() % 99) / 100.00);
            Id = "Not Set";
        }

        public Crate(string Id)
        {
            Price = (rand.Next() % 450) + 50 + ((rand.Next() % 99) / 100.00);
            this.Id = Id;
        }

        public string GetId() { return Id; }

        public void SetId(string Id)
        {
            this.Id = Id;
        }

        public double GetPrice() { return Price; }

        private void SetPrice(double Price)
        {
            this.Price = Price;
        }

        public override string ToString()
        {
            return $"Crate ID:\t {Id}"+
                $"\nCrate Price:\t ${Price}";
        }
    }
}
