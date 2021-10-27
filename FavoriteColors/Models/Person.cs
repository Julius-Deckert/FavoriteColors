using System;

namespace FavoriteColors.Models
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

        /// <summary>
        ///     Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the zip code.
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        ///     Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the favorite color.
        /// </summary>
        public string Color { get; set; }
    }
}
