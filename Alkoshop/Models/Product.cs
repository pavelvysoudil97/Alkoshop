using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Product
    {
        public Product(int id, string name, string producer, double pricePU, int amount, string availability, int alcotabac, string picture = "/Design/no_image.png")
        {
            Id = id;
            Name = name;
            Producer = producer;
            PricePU = pricePU;
            Amount = amount;
            Availability = availability;
            Alcotabac = alcotabac;
            Picture = picture;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double PricePU { get; set; } // double protoze tak je to i v DB
        public int Amount { get; set; }
        public string Availability { get; set; }
        public string Country { get; set; }
        public string Picture { get; set; }
        public int Alcotabac { get; set; } //1 - alkohol; 2 - tabak
    }
}