using System.ComponentModel.DataAnnotations;

namespace FavoriteColors.Dtos
{
    public class CreatePersonDto
    {
        public CreatePersonDto(int id, string lastName, string name, int zipCode, string city, string color)
        {
            Id = id;
            LastName = lastName;
            Name = name;
            ZipCode = zipCode;
            City = city;
            Color = color;
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
