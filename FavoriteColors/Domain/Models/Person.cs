using System;
using System.ComponentModel.DataAnnotations;

namespace FavoriteColors.Domain.Models
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string rawData)
        {
            var data = rawData.Split(",");

            //Parse personal information into properties
            Id = Convert.ToInt32(data[0]);
            LastName = data[1];
            Name = data[2];
            ZipCode = Convert.ToInt32(data[3]);
            City = data[4];
            var color = (Color)Convert.ToInt32(data[5]);
            Color = color.ToString();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
