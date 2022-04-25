﻿using CsvHelper.Configuration.Attributes;

namespace Stretto
{
    public class Flat
    {
        [Name("street")]
        public string Street { get; set; }

        [Name("city")]
        public string City { get; set; }

        [Name("zip")]
        public int Zip { get; set; }

        [Name("state")]
        public string State { get; set; }

        [Name("beds")]
        public int Beds { get; set; }

        [Name("baths")]
        public int Baths { get; set; }

        [Name("sq__ft")]
        public int Sqft { get; set; }

        [Name("type")]
        public string Type { get; set; }

        [Name("sale_date")]
        public string SaleDate { get; set; }
            
        [Name("price")]
        public double Price { get; set; }
            
        [Name("latitude")]
        public double Latitude { get; set; }

        [Name("longitude")]
        public double Longitude { get; set; }
    }
}