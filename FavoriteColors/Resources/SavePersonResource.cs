using System.ComponentModel.DataAnnotations;
using FavoriteColors.Domain.Models;

namespace FavoriteColors.Resources
{
    public class SavePersonResource
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(5)]
        public int ZipCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [EnumDataType(typeof(Color))]
        public string Color { get; set; }
    }
}
