using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Product
    {
        public Product(int id, string name, string producer, double pricePU, int amount, string availability, int alcotabac)
        {
            Id = id;
            Name = name;
            Producer = producer;
            PricePU = pricePU;
            Amount = amount;
            Availability = availability;
            Alcotabac = alcotabac;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double PricePU { get; set; }
        public int Amount { get; set; }
        public string Availability { get; set; }
        public string Country { get; set; }
        public string Picture { get; set; }
        public int Alcotabac { get; set; }
    }
}