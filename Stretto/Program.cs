using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using CsvHelper;

namespace Stretto
{
    class Program
    {
        static void Main(string[] args)
        {
            //First task
            const string CsvUrl = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/csv";
            string csv = FirstTask(CsvUrl);

            //Second task
            List<Flat> flats = SecondTask(csv);

            //Third task
            ThirdTask(flats);

            //Fourth task
            FourthTask(flats);

            //Fifth task
            const string TaxesUrl = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/taxes?city={0}";
            FifthTask(flats, TaxesUrl);
        }

        private static void FifthTask(List<Flat> flats, string TaxesUrl)
        {
            Console.WriteLine("The fifth task has started.");
            var taxes = new List<Tax>();
            var cities = flats.GroupBy(x => x.City).Select(x => x.Key);
            
            foreach (var city in cities)
            {
                using (var client = new WebClient())
                {
                    var tax = client.DownloadString(string.Format(TaxesUrl, city));
                    taxes.Add(new Tax { City = city, TaxValue = tax });
                }
            }

            foreach(var flat in flats)
            {
                var tax = taxes.Find(x => x.City.Equals(flat.City));
                flat.Price = CalculateNewPrice(flat, tax);

                PrintFlat(flat);
            }

            Console.WriteLine("The fifth task completed.");
        }

        private static double CalculateNewPrice(Flat flat, Tax tax)
        {
            double taxValue = double.Parse(tax.TaxValue, CultureInfo.InvariantCulture);
            double newPrice = (flat.Sqft * taxValue) + flat.Price;

            return newPrice;
        }

        private static void FourthTask(List<Flat> flats)
        {
            Console.WriteLine("The fourth task has started.");
            var flat = flats.OrderBy(x => x.Price).ThenByDescending(x => x.Baths + x.Beds).First();
            PrintFlat(flat);

            Console.WriteLine("The fourth task completed.");
        }

        private static void PrintFlat(Flat flat)
        {
            Console.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", flat.Street, flat.City, flat.Zip, flat.State, flat.Beds, flat.Baths, flat.Sqft, flat.Type, flat.SaleDate, flat.Price, flat.Latitude, flat.Longitude));
        }

        private static void ThirdTask(List<Flat> flats)
        {
            Console.WriteLine("The third task has started.");
            var results = flats.Where(x => x.Type.Equals("Residential")).OrderByDescending(x => x.Sqft).GroupBy(x => x.City);

            foreach (var flat in results)
            {
                PrintFlat(flat.First());
            }

            Console.WriteLine("The third task completed.");
        }

        private static List<Flat> SecondTask(string csv)
        {
            Console.WriteLine("The second task has started.");
            
            var flats = new List<Flat>();

            using(var stringReader = new StringReader(csv))
                using (var csvReader = new CsvReader(stringReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<FlatCLassMap>();
                    flats = csvReader.GetRecords<Flat>().ToList();
                }

            foreach (var flat in flats)
            {
                PrintFlat(flat);
            }

            Console.WriteLine("The second task completed.");

            return flats;
        }

        private static string FirstTask(string Url)
        {
            Console.WriteLine("The first task has started.");
            string csv;

            using (var client = new WebClient())
            {
                csv = client.DownloadString(Url);
            }

            Console.WriteLine(csv);
            Console.WriteLine("The first task completed.");

            return csv;
        }
    }
}