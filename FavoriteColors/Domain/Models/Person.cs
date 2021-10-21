using System.Collections.Generic;

namespace FavoriteColors.Domain.Models
{
    public class Person
    {
        public Person()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int ZipCode { get; set; }

        public string City { get; set; }


        public int ColorId { get; set; }

        public string Color { get; set; }
    }
}
