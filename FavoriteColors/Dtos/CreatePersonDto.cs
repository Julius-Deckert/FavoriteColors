using System.ComponentModel.DataAnnotations;

namespace FavoriteColors.Dtos
{
    public class CreatePersonDto
    {
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
