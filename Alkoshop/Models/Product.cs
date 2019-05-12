using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Product
    {
        public Product() { }

        public Product(string name, string producer, double pricePU, int amount, string availability, string description, int alcotabac)
        {
            Name = name;
            Producer = producer;
            PricePU = pricePU;
            Amount = amount;
            Availability = availability;
            Description = description;
            Alcotabac = alcotabac;
        }

        public Product(int id, string name, string producer, double pricePU, int amount, string availability, int alcotabac, string description, string country, string picture = "/Design/no_image.png")
        {
            Id = id;
            Name = name;
            Producer = producer;
            PricePU = pricePU;
            Amount = amount;
            Availability = availability;
            Alcotabac = alcotabac;
            Description = description;
            Country = country;
            Picture = picture;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double PricePU { get; set; } // double protoze tak je to i v DB
        public int Amount { get; set; }
        public string Availability { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int Alcotabac { get; set; } //1 - alkohol; 2 - tabak
        public int PriceWithDPH { get; set; }
    }
}