using CsvHelper.Configuration.Attributes;

namespace FavoriteColors.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Index(0)]
        public string Name { get; set; }

        [Index(1)]
        public string LastName { get; set; }

        [Index(2)]
        public int ZipCode { get; set; }

        public string City { get; set; }

        [Index(3)]
        public string Color { get; set; }
    }
}
