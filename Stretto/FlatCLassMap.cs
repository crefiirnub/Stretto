using CsvHelper.Configuration;

namespace Stretto
{
    public class FlatCLassMap : ClassMap<Flat>
    {
        public FlatCLassMap()
        {
            Map(m => m.Street).Name("street");
            Map(m => m.City).Name("city");
            Map(m => m.Zip).Name("zip");
            Map(m => m.State).Name("state");
            Map(m => m.Beds).Name("beds");
            Map(m => m.Baths).Name("baths");
            Map(m => m.Sqft).Name("sq__ft");
            Map(m => m.Type).Name("type");
            Map(m => m.SaleDate).Name("sale_date");
            Map(m => m.Price).Name("price"); 
            Map(m => m.Latitude).Name("latitude");
            Map(m => m.Longitude).Name("longitude");
        }
    }
}